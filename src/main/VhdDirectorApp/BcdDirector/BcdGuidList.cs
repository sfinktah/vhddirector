using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VhdDirectorApp.BcdDirector
{
    public class BcdGuidList
    {
        static protected Dictionary<Guid, GuidItem> _guidList = new Dictionary<Guid, GuidItem>();
    
        static BcdGuidList() {
			
			Add(new Guid("5189b25c-5558-4bf2-bca4-289b11bd29e2 "), 	"{badmemory}", 	
					"Global RAM defect list that can be inherited by any boot application.");
			Add(new Guid("6efb52bf-1766-41db-a6b3-0ee5eff72bd7"), 	"{bootloadersettings}", 	
					"Global settings that should be inherited by all Windows boot loader applications.");
			Add(new Guid("4636856e-540f-4170-a130-a84776f4c654 "), 	"{dbgsettings}", 	
					"Global debugger settings that can be inherited by any boot application.");
			Add(new Guid("0ce4991b-e6b3-4b16-b23c-5e0d9250e5d9 "), 	"{emssettings}", 	
					"Global Emergency Management Services settings that can be inherited by any boot application.");
			Add(new Guid("7ea2e1ac-2e61-4728-aaa3-896d9d0a9f0e"), 	"{globalsettings}", 	
					"Global settings that should be inherited by all boot applications.");
			Add(new Guid("1afa9c49-16ab-4a5c-901b-212802da9460"), 	"{resumeloadersettings}", 	
					"Global settings that should be inherited by all resume applications.");
		
        }

        static public void Add(Guid guid, String name, String description) {
            GuidItem gi = new GuidItem(name, description);
            _guidList.Add(guid, gi);
        }

        static public bool TryGetValue(Guid guid, out GuidItem item) {
            return _guidList.TryGetValue(guid, out item);
        }
    }
}
/* vim: set ts=4 sts=0 sw=4 noet: */
