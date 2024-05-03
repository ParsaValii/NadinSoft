namespace NadinSoft.Domain.Dtos;
public record ProductDto(Guid Id, string Name, DateTime ProduceDate, string ManufacturePhone, string ManufactureEmail, bool IsAvailable, Guid UserId);