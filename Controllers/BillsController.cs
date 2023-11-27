// BillsController.cs
using System;
using System.Collections.Generic;

using eStavba.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;
using System.Security.Claims;

[Authorize]
public class BillsController : Controller
{

    public IActionResult MyBills()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var bills = GetBillsForUser(userId);
        return View(bills);
    }


    [Authorize]
    private List<Bills> GetBillsForUser(string userId)
    {
     
        return new List<Bills>
        {
            new Bills { Id = 1, UserId = userId, BillType = "Electricity", Amount = 100.00M, DueDate = DateTime.Now.AddDays(7) },
            new Bills { Id = 2, UserId = userId, BillType = "Maintenance", Amount = 50.00M, DueDate = DateTime.Now.AddDays(14) },
            new Bills { Id = 3, UserId = userId, BillType = "Water", Amount = 35.00M, DueDate = DateTime.Now.AddDays(10) },
            new Bills { Id = 4, UserId = userId, BillType = "Heating", Amount = 150.00M, DueDate = DateTime.Now.AddDays(12) },
            new Bills { Id = 5, UserId = userId, BillType = "Internet & TV", Amount = 40.00M, DueDate = DateTime.Now.AddDays(2) },
        };
    }


}
