﻿using Microsoft.AspNetCore.Mvc;
using SimpleTemplate_Shop.Repository.IRepository;

namespace SimpleTemplate_Shop.Components
{
    public class AppHomeImageViewComponent : ViewComponent
    {
        private readonly IInfoRepository _infoRepository;
        public AppHomeImageViewComponent(IInfoRepository infoRepository)
        {
            _infoRepository = infoRepository;
        }
        public IViewComponentResult Invoke()
        {
            var info = _infoRepository.GetInfo(1);
            return View(info);
        }
    }
}
