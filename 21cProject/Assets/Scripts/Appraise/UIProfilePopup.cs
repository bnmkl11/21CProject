using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIProfilePopup : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI m_TextOfName;

    [SerializeField] 
    private Image m_ImageOfProfile;

    [SerializeField] 
    private TextMeshProUGUI m_TextOfTribe;
    
    [SerializeField] 
    private TextMeshProUGUI m_TextOfProfession;
    
    [SerializeField] 
    private TextMeshProUGUI m_TextOfProperty;
    
    [SerializeField] 
    private TextMeshProUGUI m_TextOfGuild;

    [SerializeField] 
    private List<TextMeshProUGUI> m_ListOfSkillName;
    
    [SerializeField] 
    private List<TextMeshProUGUI> m_ListOfCrimialHistory;

    private ProfileData m_ProfileData;

    public void UpdatePopup(ProfileData data)
    {
        m_ProfileData = data;

        m_TextOfName.text = m_ProfileData.NAME;
        m_ImageOfProfile.sprite = ResourceManager.Instance.GetSprite(m_ProfileData.IMAGE_NAME);
        m_TextOfTribe.text = m_ProfileData.TRIBE;
        m_TextOfProfession.text = m_ProfileData.PROFESSION;
        m_TextOfProperty.text = m_ProfileData.PROPERTY;
        m_TextOfGuild.text = m_ProfileData.GUILD;

        for (int i = 0; i < m_ListOfSkillName.Count; i++)
        {
            m_ListOfSkillName[i].text = m_ProfileData.SKILL[i];
        }

        for (int i = 0; i < m_ListOfCrimialHistory.Count; i++)
        {
            m_ListOfCrimialHistory[i].text = m_ProfileData.CRIMINALHISTORY[i];
        }
    }
}
