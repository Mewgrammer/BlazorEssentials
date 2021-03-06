﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazingComponents.Authentication.Models
{
    public class RegisterForm
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name needs to have between 5 and 50 characters.")]
        public string Username { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Password needs to be at least 5 characters long.")]
        public string Password { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Password needs to be at least 5 characters long.")]
        [Compare(nameof(Password), ErrorMessage ="Passwords must match.")]
        public string ConfirmPassword { get; set; } = "";


    }
}
