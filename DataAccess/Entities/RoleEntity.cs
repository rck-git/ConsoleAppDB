﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

//entity should reflect/represent the table in the database
//while the model should reflect the object in the program
public class RoleEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR (100)")]
    public string RoleName { get; set; } = null!;

    //create a one to many relationship
    //Customer can have 1 role while i.e
    // roles can have multiple users
    //public virtual ICollection<RoleEntity> Roles { get; set; } = new HashSet<RoleEntity>();

}