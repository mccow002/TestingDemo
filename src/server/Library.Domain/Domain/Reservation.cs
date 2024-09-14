namespace Library.Domain.Domain;

public class Reservation
{
    public Guid ReservationId { get; internal set; }

    public DateTime HoldDate { get; internal set; }

    public int ReservationStatusId { get; internal set; }

    public ReservationStatus ReservationStatus { get; internal set; }

    public Guid BookId { get; internal set; }

    public Book Book { get; internal set; }

    public Guid UserId { get; internal set; }

    public User User { get; internal set; }
    
    public static Reservation Create(Guid bookId, Guid userId)
    {
        return new Reservation
        {
            HoldDate = DateTime.Now,
            ReservationStatusId = ReservationStatus.Reserved.ReservationStatusId,
            BookId = bookId,
            UserId = userId
        };
    }

    public void Fulfill()
    {
        ReservationStatusId = ReservationStatus.Fulfilled.ReservationStatusId;
    }
}