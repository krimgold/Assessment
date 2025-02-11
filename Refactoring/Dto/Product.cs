namespace Refactoring.Dto
{
	public record Product
	{
		public long Id { get; set; }
		public string? Name { get; set; }
		public double Price { get; set; }
	}
}
