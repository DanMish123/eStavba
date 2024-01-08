// BillsController.cs
using System;
using System.Collections.Generic;

using eStavba.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;
using System.Security.Claims;
using eStavba.Data;

[Authorize]
public class BillsController : Controller
{

    private readonly ApplicationDbContext _context;

    public BillsController(ApplicationDbContext context)
    {
        _context = context;
    }

    private string GetCurrentUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public IActionResult GetBillsForCurrentUser()
    {
        string currentUserId = GetCurrentUserId();

        var userBills = _context.Bills.Where(b => b.UserId == currentUserId).ToList();

        return View(userBills);
    }

    public IActionResult GetBillsForUser()
    {
        string currentUserId = GetCurrentUserId(); 

        var userBills = _context.Bills.Where(b => b.UserId == currentUserId).ToList();

        return View(userBills);
    }

    public IActionResult MarkBillAsPaid(int billId)
    {
        var bill = _context.Bills.Find(billId);

        if (bill != null)
        {
            bill.IsPaid = true;
            _context.SaveChanges();
        }

        return RedirectToAction("GetBillsForCurrentUser");
    }

    [Authorize]
     public IActionResult Create()
        {
            return View();
        }


    [Authorize]
    [HttpPost]
      public IActionResult Create(Bills bill)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bill.UserId = userId;

            _context.Bills.Add(bill);
            _context.SaveChanges();

            return RedirectToAction("MyBills");
            
        }

        [Authorize]
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var billToDelete = _context.Bills.Find(id);

        if (billToDelete == null)
        {
            return NotFound(); 
        }

        _context.Bills.Remove(billToDelete);
        _context.SaveChanges();

        return RedirectToAction("MyBills");
    }


    public IActionResult ReportNewBill(string userId, string billType, decimal amount, DateTime dueDate)
    {
        var newBill = new Bills
        {
            UserId = userId,
            BillType = billType,
            Amount = amount,
            DueDate = dueDate,
            IsPaid = false
        };

        _context.Bills.Add(newBill);
        _context.SaveChanges();

        return RedirectToAction("GetBillsForUser", new { userId });
    }

    public IActionResult MyBills()
    {
        string currentUserId = GetCurrentUserId();

        var userBills = _context.Bills.Where(b => b.UserId == currentUserId).ToList();

    // Create a view model to encapsulate the data needed for the view
        var viewModel = new BillsViewModel
        {
            Bills = userBills,
        // Add other properties if needed
        };

        return View(viewModel);
    }


    // [Authorize]
    // private List<Bills> GetBillsForUser(string userId)
    // {
     
    //     return new List<Bills>
    //     {
    //         new Bills { Id = 1, UserId = userId, BillType = "Electricity", Amount = 100.00M, DueDate = DateTime.Now.AddDays(7) },
    //         new Bills { Id = 2, UserId = userId, BillType = "Maintenance", Amount = 50.00M, DueDate = DateTime.Now.AddDays(14) },
    //         new Bills { Id = 3, UserId = userId, BillType = "Water", Amount = 35.00M, DueDate = DateTime.Now.AddDays(10) },
    //         new Bills { Id = 4, UserId = userId, BillType = "Heating", Amount = 150.00M, DueDate = DateTime.Now.AddDays(12) },
    //         new Bills { Id = 5, UserId = userId, BillType = "Internet & TV", Amount = 40.00M, DueDate = DateTime.Now.AddDays(2) },
    //     };
    // }


}
