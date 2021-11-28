using Glomad.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CovidRestrictions
    {
        public int Id { get; set; }
        [Required]
        public Country Country { get; set; }
        public int Restriction { get; set; }
        public byte Level { get; set; }

        public static Dictionary<int, string> Restrictions = new Dictionary<int, string>()
        {
            { 1, "🧒 Schools closing" },
            { 2, "🏢 Workplace closing"},
            { 3, "🥳 Public events"},
            { 4, "🍻 Public gathering"},
            { 7, "😷 Face covering"},
            { 8, "🚌 Public transport"},
            { 9, "✈ Internal movement"},
            { 10, "🗺 International travel control"},
            { 11, "🧪 PCR testing"},
            { 12, "💉 Vaccination policy"},
            { 6, "🏡 Stay at home"}
        };

        public static Dictionary<int, Dictionary<byte, string>> Levels = new Dictionary<int, Dictionary<byte, string>>()
        {
            { 1, new Dictionary<byte, string>(){
                {0, "No measures"},
                {1, "Recomended"},
                {2, "Required (only at some levels)" },
                {3, "Required (all levels)" }
            } },
            { 2, new Dictionary<byte, string>()
            {
                {0, "No measures" },
                {1, "Recommended" },
                {2, "Required for some"},
                {3, "Required for all but key workers" }
            } },
            { 3, new Dictionary<byte, string>()
            {
                {0, "No measures" },
                {1, "Recommended cancellations" },
                {2, "Required cancellations"}
            } },
            { 4, new Dictionary<byte, string>()
            {
                {0, "No Restrictions" },
                {1, "Restrictions on very large gatherings (the limit is above 1000 people)" },
                {2, "gatherings between 100-1000 people"},
                {3, "gatherings between 10-100 people" },
                {4, "gatherings of less than 10 people" }
            } },
            { 7, new Dictionary<byte, string>()
            {
                {0, "No policy" },
                {1, "Recommended" },
                {2, "Required in some specified shared/public spaces outside the home with other people present, or some situations when social distancing not possible"},
                {3, "Required in all shared/public spaces outside the home with other people present or all situations when social distancing not possible" },
                {4, "Required outside the home at all times regardless of location or presence of other people" }
            } },
            { 8, new Dictionary<byte, string>()
            {
                {0, "No measures" },
                {1, "Recommended closing (or reduce volume)" },
                {2, "Required closing (or prohibit most using it)"}
            } },
            { 9, new Dictionary<byte, string>()
            {
                {0, "No measures" },
                {1, "Recommend movement restriction" },
                {2, "Restrict movement"}
            } },
            { 10, new Dictionary<byte, string>()
            {
                {0, "No measures" },
                {1, "Screening" },
                {2, "Quarantine from high-risk regions"},
                {3, "Ban on high-risk regions" },
                {4, "Total border closure" }
            } },
            { 11, new Dictionary<byte, string>()
            {
                {0, "No testing policy" },
                {1, "Testing only for those who both (a) have symptoms AND (b) meet specific criteria (e.g. key workers, admitted to hospital, came into contact with a known case, returned from overseas)" },
                {2, "Open public testing (e.g “drive through” testing available to asymptomatic people)"},
                {3, "Testing of anyone showing COVID-19 symptoms"}
            } },
            { 12, new Dictionary<byte, string>()
            {
                {0, "No availability" },
                {1, "Availability for ONE of following: key workers/ clinically vulnerable groups / elderly groups" },
                {2, "Availability for TWO of following: key workers/ clinically vulnerable groups / elderly groups"},
                {3, "Availability for ALL of following: key workers/ clinically vulnerable groups / elderly groups" },
                {4, "Availability for all three plus partial additional availability (select broad groups/ages)" },
                {5, "Universal availability" }
            } },
            { 6, new Dictionary<byte, string>()
            {
                {0, "No measures" },
                {1, "Recommended not to leave the house" },
                {2, "Required to not leave the house with exceptions for daily exercise, grocery shopping, and ‘essential’ trips"},
                {3, "Required to not leave the house with minimal exceptions (e.g. allowed to leave only once every few days, or only one person can leave at a time, etc.)" }
            } },
        };
    }
}
