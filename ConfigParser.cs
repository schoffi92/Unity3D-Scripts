/**
 * Created by István Schoffhauzer
 * 
 */


using System.IO;
using System.Collections;
using System.Collections.Generic;

public struct ConfigEntry
{

    public string section;

    public string key;

    public string value;

    /**
	 * ConfigEntry
	 * 
	 * @param string section
	 * @param string key
	 * @param string value
	 */
    public ConfigEntry(string key, string value = "", string section = "")
    {
        this.section = section.ToLower().Trim();
        this.key = key.ToLower().Trim();
        this.value = value;
    }

    /**
	 * Set Value
	 * 
	 * @param string value
	 */
    public void SetValue(string value)
    {
        this.value = value;
    }
}

public class ConfigParser
{

    /**
	 * Default Section 
	 */
    public const string DEFAULT_SECTION = "default";

    /**
	 * List Of Created Sections
	 */
    protected List<string> sections = new List<string>();

    /**
	 * List Of Entries
	 */
    protected List<ConfigEntry> entries = new List<ConfigEntry>();

    /**
	 *  Load File
	 * 
	 * @param string filename
	 * @param boll clear
	 * @return bool
	 */
    public bool LoadFile(string filename, bool clear = true)
    {
        if (!File.Exists(filename))
        {
            return false;
        }

        string content = System.IO.File.ReadAllText(filename);

        return LoadFromString(content, clear);
    }

    /**
	 * Clear Config
	 */
    public void Clear()
    {
        entries.Clear();
    }

    /**
	 * Parse String
	 * 
	 * @param string content
	 * @param bool clear - clears current config 
	 * @return bool
	 */
    public bool LoadFromString(string content, bool clear = true)
    {
        if (clear)
        {
            Clear();
        }

        string section = DEFAULT_SECTION;
        string[] lines = content.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (line.Length > 0)
            {
                switch (line[0])
                {

                    case ';':
                    case '#':
                        // These lines are comments
                        // nop
                        break;

                    case '[':
                        // Set section
                        section = line.Substring(1, line.Length - 2);
                        break;

                    default:
                        int eqChar = line.IndexOf("=");

                        if (eqChar >= 0)
                        {
                            string key = line.Substring(0, eqChar);
                            string val = line.Substring(eqChar + 1);

                            Set(key, val, section);
                        }
                        break;
                }
            }
        }

        return true;
    }

    /**
	 * Get Section
	 * 
	 * @param string section
	 * @return List<ConfigEntry>
	 */
    public List<ConfigEntry> Get(string section)
    {
        List<ConfigEntry> result = new List<ConfigEntry>();

        for (int i = 0; i < entries.Count; i++)
        {
            if (entries[i].section == section)
            {
                result.Add(entries[i]);
            }
        }

        return result;
    }

    /**
	 * Check Section Is Exists
	 * 
	 * @return bool
	 */
    public bool IsSectionExists(string section)
    {
        return sections.IndexOf(section.Trim()) >= 0;
    }

    /**
	 * Get Index
	 * 
	 * Return with -1 if entry not exists otherwise it will give back the entry's index
	 * 
	 * @param string key
	 * @param string section = "default"
	 * @return int
	 */
    public int GetIndex(string key, string section = DEFAULT_SECTION)
    {
        for (int i = 0; i < entries.Count; i++)
        {
            if (entries[i].section == section.ToLower().Trim() && entries[i].key == key.ToLower().Trim())
            {
                return i;
            }
        }

        return -1;
    }

    /**
	 * Get
	 * 
	 * If entry not exists returns with defaultVal
	 * If entry exists returns with it's value
	 * 
	 * @param string key
	 * @param string section = "default"
	 * @param string defaultVal = ""
	 * @return string
	 */
    public string Get(string key, string section = DEFAULT_SECTION, string defaultVal = "")
    {
        int index = GetIndex(key, section);

        if (index >= 0)
        {
            return entries[index].value;
        }

        return defaultVal;
    }

    /**
	 * Set Entry
	 * 
	 * Create Or Change an entry
	 * 
	 * @param string key
	 * @param string section = "default"
	 */
    public void Set(string key, string value, string section = DEFAULT_SECTION)
    {
        int index = GetIndex(key, section);

        // Entry Exists
        if (index >= 0)
        {
            entries.RemoveAt(index);
        }
        else
        {
            if (!IsSectionExists(section))
            {
                sections.Add(section.Trim());
            }
        }

        // Entry Not Exists
        ConfigEntry e = new ConfigEntry(section, key, value);
        entries.Add(e);
    }

    /**
	 * Get Created Sections
	 */
    public string[] GetSections()
    {
        return sections.ToArray();
    }

    /**
	 * To Array
	 */
    public ConfigEntry[] ToArray()
    {
        return entries.ToArray();
    }

    /**
	 * Generate Config String
	 */
    public override string ToString()
    {
        string result = "";
        for (int i = 0; i < sections.Count; i++)
        {
            List<ConfigEntry> e = Get(sections[i]);

            result += "[" + sections[i] + "]\n";
            for (int j = 0; j < e.Count; j++)
            {
                result += e[j].key + "=" + e[j].value + "\n";
            }
        }

        return result;
    }
}
