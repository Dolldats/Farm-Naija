namespace Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public User User { get; set; } = null!;
    }
}
