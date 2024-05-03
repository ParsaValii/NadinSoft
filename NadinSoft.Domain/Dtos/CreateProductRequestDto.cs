namespace NadinSoft.Domain.Dtos;
public record CreateProductRequestDto(string Name, DateTime ProduceDate, string ManufacturePhone, string ManufactureEmail, bool IsAvailable);