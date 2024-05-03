namespace NadinSoft.Domain.Dtos;
public record UpdateProductRequestDto(string Name, DateTime ProduceDate, string ManufacturePhone, string ManufactureEmail, bool IsAvailable);