using System;
using System.Collections.Generic;

namespace MysqlWebAPI.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedTs { get; set; }
}
