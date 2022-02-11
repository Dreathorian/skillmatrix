namespace SkillMatrixAPITest.DTOs;

public record CreateUserDTO(string  Email, string Password, string? FirstName = null,
                            string? LastName = null);