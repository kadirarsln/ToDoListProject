﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Tokens.Configs;
using ToDoList.Models.Dtos.Tokens;
using ToDoList.Models.Entities;
using ToDoList.Service.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Service.Concretes;

public sealed class JwtService : IJwtService
{
    private readonly TokenOption _tokenOption;
    private readonly UserManager<User> _userManager;

    public JwtService(IOptions<TokenOption> tokenOption, UserManager<User> userManager)
    {
        _tokenOption = tokenOption.Value;
        _userManager = userManager;
    }

    public async Task<TokenResponseDto> CreateTokenAsync(User user)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
        var secretKey = SecretKeyHelper.GetSecurityKey(_tokenOption.SecurityKey);

        SigningCredentials sc = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _tokenOption.Issuer,
            claims: await GetClaims(user, _tokenOption.Audience),
            expires: accessTokenExpiration,
            signingCredentials: sc
            );
        var handler = new JwtSecurityTokenHandler();
        string token = handler.WriteToken(jwtSecurityToken);

        return new TokenResponseDto
        {
            AccessToken = token,
            AccessTokenExpiration = accessTokenExpiration,
        };    }

    public async Task<List<Claim>> GetClaims(User user, List<string> audiences)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim  ("email",user.Email),
            new Claim(ClaimTypes.Name,user.UserName),
        };

        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Count > 0)
        {
            claims.AddRange(roles.Select(x=> new Claim(ClaimTypes.Role,x)));
        }

        claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        return claims;
    }

}
