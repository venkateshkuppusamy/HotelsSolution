using FluentValidation;

namespace HotelsAPI.Models
{
    public class Hotel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Rating { get; set; }
    }

    public class HotelFilter : BaseFilter
    {
        public string? Name { get; set; }
        public int? Rating { get; set; }
    }

    public class HotelFilterValidator : AbstractValidator<HotelFilter>
    {
        public HotelFilterValidator() {
            RuleFor(r => r.Rating).InclusiveBetween(1, 5);
        }
    }
    
}
