﻿using UrlShortener.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Models
{
    public interface IUrlManager
    {
        Task<ShortUrl> ShortenUrl(string longUrl, string ip, string segment = "");
        Task<Stat> Click(string segment, string referer, string ip);
    }
}