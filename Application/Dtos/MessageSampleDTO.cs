﻿namespace Application.Dtos;

public class MessageSampleDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}