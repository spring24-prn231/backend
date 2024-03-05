﻿using BusinessObjects.Models;
using BusinessObjects.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace BirthdayBlitzAPI.Validators
{
    public class CreateRoomValidator : AbstractValidator<CreateRoomRequest>
    {
        private readonly IRoomService _roomService;
        public CreateRoomValidator(IRoomService roomService)
        {
            _roomService = roomService;

            RuleForEach(x => x.Slots)
                .ChildRules(slot =>
                {
                    slot.RuleFor(s => s.FromHour)
                        .NotEmpty()
                        .WithMessage("Giờ bắt đầu bắt buộc")
                        .Must(BeValidHour).WithMessage("Thời gian phải trong khoảng từ 0h đến 24h");

                    slot.RuleFor(s => s.ToHour)
                        .NotEmpty().WithMessage("Giờ kết thúc là bắt buộc")
                        .Must(BeValidHour).WithMessage("Thời gian phải trong khoảng từ 0h đến 24h")
                        .GreaterThan(x => x.FromHour).WithMessage("Giờ kết thúc phải lớn hơn giờ bắt đầu");
                });
        }

        private bool BeValidHour(string hour)
        {
            if (!int.TryParse(hour, out int hourValue))
            {
                return false;
            }
            return hourValue >= 0 && hourValue <= 24;
        }
    }
}
