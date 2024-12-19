namespace conferencePlannerCore.Models
{
    public class Criteria
    {
		private static readonly Random _random = new Random();
		public int Id { get; init; } = _random.Next(1, int.MaxValue);
		public string Name { get; set; } = string.Empty;
        public int? Grade { get; set; }
    }
}
