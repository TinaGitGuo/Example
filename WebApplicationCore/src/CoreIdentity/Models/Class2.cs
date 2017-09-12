using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Models
{
    public class Allowance
    {
        public const int MaxClientIdLength = 50;
        public const int MaxUsedAllowanceIDLength = 10;
        public const int MaxAllowanceTypeLength = 2;
        public const int MaxDescriptionLength = 35;
        public const int MaxPaidByLength = 10;
        public const int MaxAmountLength = 10;
        public const int MaxTaxableLength = 4;
        public const int MaxW1Length = 4;
        public const int MaxPayrollTaxLength = 4;
        public const int MaxOTELength = 4;
        public const int MaxSGRuleLength = 4;
        public const int MaxSpecialSuperRuleLength = 4;
        public const int MaxSpecialSuperRateLength = 11;

        public int id { get; set; }
        [Required]
        [MaxLength(MaxClientIdLength)]
        public string ClientId { get; set; }
        [Required]
        public int UsedAllowanceID { get; set; }
        [Required]
        [MaxLength(MaxAllowanceTypeLength)]
        public string AllowanceType { get; set; }
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
        [Required]
        public int PaidBy { get; set; }
        [Required]
        [MaxLength(MaxAmountLength)]
        public string Amount { get; set; }
        [Required]
        public Boolean Taxable { get; set; }
        [Required]
        public Boolean W1 { get; set; }
        [Required]
        public Boolean PayrollTax { get; set; }
        [Required]
        public Boolean OTE { get; set; }
        [Required]
        public Boolean SGRule { get; set; }
        [Required]
        public Boolean SpecialSuperRule { get; set; }
        [Required]
        public float SpecialSuperRate { get; set; }
    }
}
