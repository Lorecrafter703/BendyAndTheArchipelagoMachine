using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Data
{
    internal struct IDTables
    {
        private static readonly Dictionary<long, string> itemIDtoName = new Dictionary<long, string>
        {
            { 1, "Bacon Soup" },
            { 100, "Unlock CH1" },
            { 101, "CH1 Book" },
            { 102, "CH1 Doll" },
            { 103, "CH1 Gear" },
            { 104, "CH1 Inkwell" },
            { 105, "CH1 Record" },
            { 106, "CH1 Wrench" },
            { 200, "Unlock CH2" },
            { 201, "CH2 Keys" },
            { 202, "CH2 Valve" },
            { 300, "Unlock CH3" },
            { 301, "CH3 Toys" },
            { 400, "Unlock CH4" },
            { 401, "CH4 Books" },
            { 402, "CH4 Bossfight Bertrum" },
            { 500, "Unlock CH5" },
        };

        private static readonly Dictionary<string, long> locationNametoID = new Dictionary<string, long>
        {

            { "CH1 Bacon Soup 0", 100 },
            { "CH1 Bacon Soup 1", 101 },
            { "CH1 Bacon Soup 2", 102  },
            { "CH1 Bacon Soup 3", 103 },
            { "CH1 Bacon Soup 4", 104 },
            { "CH1 Bacon Soup 5", 105 },
            { "CH1 Bacon Soup 6", 106 },
            { "CH1 Bacon Soup 7", 107 },
            { "CH1 Bacon Soup 8", 108 },
            { "CH1 Bacon Soup 9", 109 },
            { "CH1 Bacon Soup 10", 110 },
            { "CH1 Bacon Soup 11", 111 },
            { "CH1 Bacon Soup 12", 112 },
            { "CH1 Bacon Soup 13", 113 },
            { "CH1 Bacon Soup 14", 114 },
            { "CH1 Bacon Soup 15", 115 },
            { "CH1 Bacon Soup 16", 116 },
            { "CH1 Bacon Soup 17", 117  },
            { "CH1 Bacon Soup 18", 118 },
            { "CH1 Bacon Soup 19", 119 },
            { "CH1 Bacon Soup 20", 120 },
            { "CH1 Book", 121 },
            { "CH1 Doll", 122 },
            { "CH1 Gear", 123 },
            { "CH1 Inkwell", 124 },
            { "CH1 Record", 125 },
            { "CH1 Wrench", 126 },
            { "CH1 Audio Log Thomas 1", 127 },
            { "CH1 Audio Log Wally 1", 128 },
            { "CH1 Complete", 199 },
            { "CH2 Bacon Soup 0", 200 },
            { "CH2 Bacon Soup 1", 201 },
            { "CH2 Bacon Soup 2", 202 },
            { "CH2 Bacon Soup 3", 203 },
            { "CH2 Bacon Soup 4", 204 },
            { "CH2 Bacon Soup 5", 205 },
            { "CH2 Bacon Soup 6", 206 },
            { "CH2 Bacon Soup 7", 207 },
            { "CH2 Bacon Soup 8", 208 },
            { "CH2 Bacon Soup 9", 209 },
            { "CH2 Bacon Soup 10", 210 },
            { "CH2 Bacon Soup 11", 211 },
            { "CH2 Bacon Soup 12", 212 },
            { "CH2 Bacon Soup 13", 213 },
            { "CH2 Bacon Soup 14", 214 },
            { "CH2 Bacon Soup 15", 215 },
            { "CH2 Bacon Soup 16", 216 },
            { "CH2 Bacon Soup 17", 217 },
            { "CH2 Bacon Soup 18", 218 },
            { "CH2 Bacon Soup 19", 219 },
            { "CH2 Bacon Soup 20", 220 },
            { "CH2 Bacon Soup 21", 221 },
            { "CH2 Bacon Soup 22", 222 },
            { "CH2 Bacon Soup 23", 223 },
            { "CH2 Bacon Soup 24", 224 },
            { "CH2 Bacon Soup 25", 225 },
            { "CH2 Bacon Soup 26", 226 },
            { "CH2 Bacon Soup 27", 227 },
            { "CH2 Bacon Soup 28", 228 },
            { "CH2 Bacon Soup 29", 229 },
            { "CH2 Bacon Soup 30", 230 },
            { "CH2 Keys", 231 },
            { "CH2 Valve", 232 },
            { "CH2 Audio Log The Prayer", 233 },
            { "CH2 Audio Log Distractions", 234 },
            { "CH2 Audio Log The New Voice Actress", 235 },
            { "CH2 Audio Log The Projectionist", 236 },
            { "CH2 Audio Log Lost Key", 237 },
            { "CH2 Audio Log Favorite Song", 238 },
            { "CH2 Audio Log Jack Fain", 239 },
            { "CH2 Complete", 299 },
            { "CH3 Bacon Soup 0", 300 },
            { "CH3 Bacon Soup 1", 301 },
            { "CH3 Bacon Soup 2", 302 },
            { "CH3 Bacon Soup 3", 303 },
            { "CH3 Bacon Soup 4", 304 },
            { "CH3 Bacon Soup 5", 305 },
            { "CH3 Bacon Soup 6", 306 },
            { "CH3 Bacon Soup 7", 307 },
            { "CH3 Bacon Soup 8", 308 },
            { "CH3 Bacon Soup 9", 309 },
            { "CH3 Bacon Soup 10", 310 },
            { "CH3 Bacon Soup 11", 311 },
            { "CH3 Bacon Soup 12", 312 },
            { "CH3 Bacon Soup 13", 313 },
            { "CH3 Bacon Soup 14", 314 },
            { "CH3 Bacon Soup 15", 315 },
            { "CH3 Bacon Soup 16", 316 },
            { "CH3 Bacon Soup 17", 317 },
            { "CH3 Bacon Soup 18", 318 },
            { "CH3 Bacon Soup 19", 319 },
            { "CH3 Bacon Soup 20", 320 },
            { "CH3 Bacon Soup 21", 321 },
            { "CH3 Bacon Soup 22", 322 },
            { "CH3 Bacon Soup 23", 323 },
            { "CH3 Bacon Soup 24", 324 },
            { "CH3 Bacon Soup 25", 325 },
            { "CH3 Bacon Soup 26", 326 },
            { "CH3 Bacon Soup 27", 327 },
            { "CH3 Bacon Soup 28", 328 },
            { "CH3 Bacon Soup 29", 329 },
            { "CH3 Bacon Soup 30", 330 },
            { "CH3 Bacon Soup 31", 331 },
            { "CH3 Bacon Soup 32", 332 },
            { "CH3 Bacon Soup 33", 333 },
            { "CH3 Bacon Soup 34", 334 },
            { "CH3 Bacon Soup 35", 335 },
            { "CH3 Bacon Soup 36", 336 },
            { "CH3 Bacon Soup 37", 337 },
            { "CH3 Bacon Soup 38", 338 },
            { "CH3 Audio Log Shawn Crooked", 350 },
            { "CH3 Audio Log Joey Drew Belief", 351 },
            { "CH3 Audio Log Susie Apart", 352 },
            { "CH3 Audio Log Wally Thomas", 353 },
            { "CH3 Audio Log Thomas", 354 },
            { "CH3 Audio Log Susie Lunch", 355 },
            { "CH3 Audio Log Wally Smile", 356 },
            { "CH3 Audio Log Grant Genius", 357 },
            { "CH3 Audio Log Norman Trouble", 358 },
            { "CH3 Audio Log Henry", 359 },
            { "CH3 Complete", 399 },
            { "CH4 Bacon Soup 0", 400 },
            { "CH4 Bacon Soup 1", 401 },
            { "CH4 Bacon Soup 2", 402 },
            { "CH4 Bacon Soup 3", 403 },
            { "CH4 Bacon Soup 4", 404 },
            { "CH4 Bacon Soup 5", 405 },
            { "CH4 Bacon Soup 6", 406 },
            { "CH4 Bacon Soup 7", 407 },
            { "CH4 Bacon Soup 8", 408 },
            { "CH4 Bacon Soup 9", 409 },
            { "CH4 Bacon Soup 10", 410 },
            { "CH4 Bacon Soup 11", 411 },
            { "CH4 Bacon Soup 12", 412 },
            { "CH4 Bacon Soup 13", 413 },
            { "CH4 Bacon Soup 14", 414 },
            { "CH4 Bacon Soup 15", 415 },
            { "CH4 Bacon Soup 16", 416 },
            { "CH4 Bacon Soup 17", 417 },
            { "CH4 Bacon Soup 18", 418 },
            { "CH4 Bulls Eye", 419 },
            { "CH4 Call the Milk Man", 420 },
            { "CH4 Wasting Time", 421 },
            { "CH4 Bertrum Boss", 422 },
            { "CH4 Brute Boris Boss", 423 },
            { "CH4 Audio Log Grant Transform", 430 },
            { "CH4 Audio Log Susie Transform", 431 },
            { "CH4 Audio Log Bert Transform", 432 },
            { "CH4 Audio Log Wally Transform", 433 },
            { "CH4 Audio Log Lacie Transform", 434 },
            { "CH4 Audio Log Bert Boss", 435 },
            { "CH4 Audio Log Joey Transform", 436 },
            { "CH4 Complete", 499 },
        };

        public static string GetItemName(long id)
        {
            try
            {
                return itemIDtoName[id];
            }
            catch (Exception e)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError($"No item found at id {id}.\n    {e.Message}");
                return null;
            }
        }

        public static long GetLocationID(string name)
        {
            try
            {
                return locationNametoID[name];
            }
            catch (Exception e)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError($"No location found with name {name}.\n    {e.Message}");
                return -1;
            }
        }
    }
}
