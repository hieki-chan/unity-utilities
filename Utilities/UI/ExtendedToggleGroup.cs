using System.Collections.Generic;
using UnityEngine.UI;

namespace Utilities.UI
{
    /// <summary>
    /// ToggleGroup but can get registered toggles list
    /// </summary>
    public class ExtendedToggleGroup : ToggleGroup
    {
        public List<Toggle> Toggles => m_Toggles;
    }
}