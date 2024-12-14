using System.ComponentModel.DataAnnotations;

namespace conferencePlannerCore.Models
{
    public record Abstract
    {
        public int Id { get; set; }

        public int ConferenceId { get; set; }

        [Required(ErrorMessage = "Navn er påkrævet")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Navnet skal være mellem 2 og 100 tegn")]
        public string SenderName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail er påkrævet")]
        [EmailAddress(ErrorMessage = "Ugyldig e-mail adresse")]
        [StringLength(100, ErrorMessage = "E-mail kan ikke være længere end 100 tegn")]
        public string PresenterEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Organisation er påkrævet")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Organisation skal være mellem 2 og 200 tegn")]
        public string Organization { get; set; } = string.Empty;

        [Required(ErrorMessage = "Titel er påkrævet")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Titlen skal være mellem 5 og 250 tegn")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nøgleord er påkrævet")]
        [StringLength(400, MinimumLength = 10, ErrorMessage = "Nøgleord skal være mellem 10 og 400 tegn")]
        public string KeyValues { get; set; } = string.Empty;

        [Required(ErrorMessage = "Abstract tekst er påkrævet")]
        [StringLength(2000, MinimumLength = 100, ErrorMessage = "Abstract tekst skal være mellem 100 og 2000 tegn")]
        public string AbstractText { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kategori er påkrævet")]
        public string Category { get; set; } = string.Empty;

        public string Picture { get; set; } = string.Empty;

        public List<string> CoAuthors { get; set; } = new();

        public List<Review> Reviews { get; set; } = new();

        // Custom validation method for CoAuthors
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CoAuthors != null && CoAuthors.Any(author => author.Length > 100))
            {
                yield return new ValidationResult(
                    "Hver medforfatter må ikke overstige 100 tegn",
                    new[] { nameof(CoAuthors) }
                );
            }

            if (CoAuthors != null && CoAuthors.Count > 10)
            {
                yield return new ValidationResult(
                    "Der må maksimalt være 10 medforfattere",
                    new[] { nameof(CoAuthors) }
                );
            }
        }
    }
}