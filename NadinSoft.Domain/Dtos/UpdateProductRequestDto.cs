using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NadinSoft.Domain.Dtos;
public record UpdateProductRequestDto(string Name, DateTime ProduceDate, string ManufacturePhone, string ManufactureEmail, bool IsAvailable);