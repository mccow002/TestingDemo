namespace Library.Domain.Domain;

public class ReservationStatus
{
    public static readonly ReservationStatus Reserved = new(1, "Reserved");
    public static readonly ReservationStatus Fulfilled = new(2, "Fulfilled");

    internal ReservationStatus(int reservationStatusId, string status)
    {
        ReservationStatusId = reservationStatusId;
        Status = status;
    }
    
    public int ReservationStatusId { get; internal set; }

    public string Status { get; internal set; }

    public ICollection<Reservation> Reservations { get; set; } = [];
}