﻿namespace dotNet_RESTful_Web_API.models.Dto;

public class RegistrationRequestDto
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}