﻿using Entity.Lawfullingo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Lawfullingo.Repository.Courses;

public class CourseRepository
{
    private readonly ApplicationDbContext _context;

    public CourseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses
                       .Include(c => c.category)
                       .Include(c => c.teachers)
                       .Include(c => c.purchase)
                       .ToListAsync();
    }

    public async Task<Course> GetByIdAsync(int id)
    {
        return await _context.Courses
                       .Include(c => c.category)
                       .Include(c => c.teachers)
                       .Include(c => c.purchase)
                       .FirstOrDefaultAsync(c => c.id == id);
    }

    public async Task AddAsync(Course course)
    {
       await _context.Courses.AddAsync(course);
      await  _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
      await  _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
           await _context.SaveChangesAsync();
        }
    }
} 