namespace Library.Domain.Domain;

public class Reservation
{
    public Guid ReservationId { get; set; }

    public DateTime HoldDate { get; set; }

    public string Status { get; set; }

    public Guid BookId { get; set; }

    public Book Book { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }
}