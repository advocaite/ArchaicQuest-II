﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchaicQuestII.Core.Character;
using Microsoft.AspNetCore.Mvc;
using ArchaicQuestII.Core.Room;
using ArchaicQuestII.Core.Events;
using ArchaicQuestII.Core.Item;
using Microsoft.Azure.KeyVault.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArchaicQuestII.Controllers
{
    public class MobController : Controller
    {

        [HttpPost]
        [Route("api/mob/PostMob")]
        public void PostMob([FromBody] Character mob)
        {


            if (!ModelState.IsValid)
            {
                var exception = new Exception("Invalid mob");
                throw exception;
            }

            var newMob = new Character()
            {
                Name = mob.Name,
                Level = mob.Level,
                ArmorRating = new ArmourRating()
                {
                    Armour = mob.ArmorRating.Armour,
                    Magic = mob.ArmorRating.Magic
                },
                Affects = mob.Affects,
                AlignmentScore = mob.AlignmentScore,
                Attributes = mob.Attributes,
                MaxAttributes = mob.Attributes,
                ClassName = mob.ClassName,
                Config = null,
                Description = mob.Description,
                Gender = mob.Gender,
                Stats = mob.Stats,
                MaxStats = mob.Stats,
                Money = mob.Money,
                Race = mob.Race,
            };


            if (!string.IsNullOrEmpty(mob.Id.ToString()) && mob.Id != -1)
            {

                var foundItem = DB.GetMob(mob.Id.ToString());

                if (foundItem == null)
                {
                    throw new Exception("mob Id does not exist");
                }

                newMob.Id = mob.Id;
            }



            DB.SaveMob(newMob);

        }


        //[HttpGet]
        //[Route("api/mob/Get")]
        //public List<Character> GetMob()
        //{

        //    var mobs = DB.GetItems();

        //    return mobs;

        //}


        //[HttpGet]
        //[Route("api/mob/FindMobs")]
        //public List<Character> FindMobs([FromQuery] string query)
        //{

        //    var mobs = DB.GetItems().Where(x => x.Name != null);



        //    if (string.IsNullOrEmpty(query))
        //    {
        //        return mobs.ToList();
        //    }

        //    return mobs.Where(x => x.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) != -1).ToList();

        //}

        //[HttpGet]
        //[Route("api/mob/FindMobById")]
        //public Character FindMobById([FromQuery] int id)
        //{

        //    return DB.GetItems().FirstOrDefault(x => x.Id.Equals(id));

        //}




    }
}
