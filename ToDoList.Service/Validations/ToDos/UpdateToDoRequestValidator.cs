
using FluentValidation;
using ToDoList.Models.Dtos.ToDos.Requests;

namespace ToDoList.Service.Validations.ToDos;

public class UpdateToDoRequestValidator : AbstractValidator<UpdateToDoRequest>
{
    public UpdateToDoRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID boş olamaz.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir.");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate).WithMessage("Başlangıç tarihi bitiş tarihinden önce olmalıdır.")
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Başlangıç tarihi bugünden önce olamaz.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("Bitiş tarihi başlangıç tarihinden sonra olmalıdır.");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Geçerli bir öncelik değeri seçilmelidir.");

        RuleFor(x => x.Completed)
            .NotNull().WithMessage("Tamamlanma durumu belirtilmelidir.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Geçerli bir kategori kimliği belirtilmelidir.");
    }
}