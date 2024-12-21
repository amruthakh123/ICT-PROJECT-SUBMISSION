using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace restrauntBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] TurfUser user)
        {
            if (await _context.TurfUsers.AnyAsync(u => u.email == user.email))
            {
                return BadRequest("User with this email already exists.");
            }

            _context.TurfUsers.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { UserId = user.userid });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] TurfUser loginRequest)
        {
            var user = await _context.TurfUsers
                .FirstOrDefaultAsync(u => u.email == loginRequest.email && u.password == loginRequest.password && u.usertype==loginRequest.usertype);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new { UserId = user.userid });
        }

        [HttpGet("users")] 
        public async Task<IActionResult> GetAllUsers() 
        {
            var users = await _context.TurfUsers.ToListAsync(); 
            return Ok(users);
        }


        [HttpPost("addSlot")] public async Task<IActionResult> AddTable([FromBody] Slot request)
        { 

            var slot = new Slot
            { 
                slottime = request.slottime,
                slotdate = request.slotdate,
                status="1"
            }; 
            _context.Slots.Add(slot);
            await _context.SaveChangesAsync();
            return Ok(new { slotid = slot.slotid }); 
        }

       

        [HttpGet("allSlots")] public async Task<IActionResult> GetAllSlots()
        { 
            var tables = await _context.Slots.ToListAsync(); 
            return Ok(tables);
        }

        [HttpGet("liveslots")]
        public async Task<IActionResult> GetLiveSlots()
        {
            var tables = await _context.Slots.Where(b => b.status == "1").ToListAsync();
            return Ok(tables);
        }


        [HttpPost("bookSlot")]
        public async Task<IActionResult> CreateBooking([FromBody] SlotBooking slt)
        {
            _context.SlotBookings.Add(slt);

            var slot = await _context.Slots.FindAsync(slt.slotid);
            if (slot != null)
            {
                slot.status = "2";
                _context.Slots.Update(slot);
            }

            await _context.SaveChangesAsync();
            return Ok(new { newbookingid = slt.bookingid });
        }



        [HttpPut("resetBookStatus")]
        public async Task<IActionResult> ResetBookStatus(int slotid)
        {
            var slot = await _context.Slots.FindAsync(slotid);
            if (slot == null)
            {
                return NotFound(new { message = "table not found" });
            }

            slot.status = "1";
            _context.Slots.Update(slot);
            await _context.SaveChangesAsync();

            // Delete records from TableBookings table using tableid
            var bookings = _context.SlotBookings.Where(b => b.slotid == slotid);
            _context.SlotBookings.RemoveRange(bookings);
            await _context.SaveChangesAsync();

            return Ok(new { message = "table status reset to 1 successfully and bookings deleted", changedtableid = slotid });
        }


        [HttpGet("allBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _context.SlotBookings
                .Join(_context.Slots,
                      booking => booking.slotid,
                      slot => slot.slotid,
                      (booking, slot) => new
                      {
                          booking.custname,
                          booking.slotid,
                          slot.slottime,
                          slot.slotdate
                      })
                .ToListAsync();

            return Ok(bookings);
        }






    }



}
