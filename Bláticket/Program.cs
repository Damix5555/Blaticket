using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
// eseményekkel kezdeni >>> utána user, CRUD
//objektum orientált
//use case diagram visualparadgrim
//nincs még jegyvásárlás
namespace Bláticket
{
    internal class Program
    {
        // Static lists to store events and tickets
        static List<Event> Events = new List<Event>();
        static List<Ticket> Tickets = new List<Ticket>();

        static void Main(string[] args)
        {
            LoadEvents("events.txt");

            while (true)
            {
                Console.WriteLine("\n--- Jegyértékesítő ---");
                Console.WriteLine("1) Események listázása");
             // Console.WriteLine("2) Jegyvásárlás");
                Console.WriteLine("2) Eladott jegyek listázása");
                Console.Write("Választás: ");
                var val = Console.ReadLine();

                // Menü választás switchel
                switch (val)
                {
                    case "1": ListEvents(); break;
                 // case "2": BuyTicket(); break;
                    case "2": ListTickets(); break;
                    default: Console.WriteLine("Nincs ilyen menüpont."); break;
                }
            }
        }

        // Események betöltése fájlból
        static void LoadEvents(string path)
        {
            foreach (var line in File.ReadAllLines(path))
            {
                var parts = line.Split(';');
                if (parts.Length < 6) continue;

                Events.Add(new Event
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    Description = parts[2],
                    Date = DateTime.Parse(parts[3]),
                    Capacity = int.Parse(parts[4]),
                    Price = decimal.Parse(parts[5])
                });
            }
        }

        static void ListEvents()
        {
            foreach (var e in Events)
            {
                // Eladott jegyek száma az adott eseményhez
                int sold = Tickets.FindAll(t => t.EventId == e.Id).Count;
                // Esemény adatok kiírása
                Console.WriteLine($"ID:{e.Id} | {e.Name} | {e.Date} | Ár: {e.Price} Ft | Szabadhely: {e.Capacity - sold}/{e.Capacity}");
                Console.WriteLine($"   {e.Description}");
            }
        }

        // Eladott jegyek listázása
        static void ListTickets()
        {
            if (Tickets.Count == 0)
            {
                Console.WriteLine("Még nincs eladott jegy.");
                return;
            }
            foreach (var t in Tickets)
            {
                var ev = Events.Find(e => e.Id == t.EventId);
                // Jegy adatok kiírása
                Console.WriteLine($"{t.Id} | {ev.Name} | {t.Buyer} | {t.Price} Ft | {t.PurchasedAt}");
            }
        }
    }

    class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
    }

    class Ticket
    {
        public string Id { get; set; }
        public int EventId { get; set; }
        public string Buyer { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchasedAt { get; set; }
    }
}