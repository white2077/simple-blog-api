﻿namespace AspNetCoreRestfulApi.Dto.Response;

public class TokenResponseDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; } 
    
}