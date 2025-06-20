﻿using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public enum UserRole { User, Admin }

    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public UserRole Role { get; set; } = UserRole.User;
    }
}
