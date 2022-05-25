﻿namespace PMS.Backend.Features.Frontend.Agency.Models.Input;

public record AgencyContactDTO(
    string ContactName,
    string? Email,
    string? Phone,
    string? Address,
    string? City,
    string? ZipCode,
    bool IsFrequentVendor)
{
    public string ContactName { get; set; } = ContactName;
    public string? Email { get; set; } = Email;
    public string? Phone { get; set; } = Phone;
    public string? Address { get; set; } = Address;
    public string? City { get; set; } = City;
    public string? ZipCode { get; set; } = ZipCode;
    public bool IsFrequentVendor { get; set; } = IsFrequentVendor;
}