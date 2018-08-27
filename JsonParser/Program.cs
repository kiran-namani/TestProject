using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using JsonParser.Models;
using Newtonsoft.Json;

namespace JsonParser
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\Pet.json");

      var petInfoAllText = System.IO.File.ReadAllText(path);

      var petOwners = JsonConvert.DeserializeObject<List<PetOwner>>(petInfoAllText);

     // print male owner pet Cat names
      Console.WriteLine("Male");
      PrintPetNames(petOwners, "Male","Cat");

      // print female owner pet Cat names
      Console.WriteLine("Female");
      PrintPetNames(petOwners, "Female","Cat");
    }

    private static void PrintPetNames(IEnumerable<PetOwner> petOwners, string gender, string petType)
    {
            var owners = petOwners.Where(po => po.gender == gender).ToList();
            List<Pet> pets = new List<Pet>();
            foreach (var petOwner in owners)
            {
                List<Pet> tempPet = petOwner.pets?.Where(p => p.type == petType).ToList();
                if (tempPet != null)
                    pets.AddRange(tempPet);
            }

            pets = pets.OrderBy(pet => pet.name).ToList();
            foreach (var pet in pets)
            {
                Console.WriteLine($".   {pet.name}");
            }
    }
  }
}