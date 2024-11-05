﻿using Core.Entities;
using ToDoList.Models.Enums;

namespace ToDoList.Models.Entities;

public class ToDo : Entity<Guid>
{
    public ToDo()
    {
        Title=string.Empty;
        Description=string.Empty;
    }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Priority Priority { get; set; }
    public bool Completed { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
}
