using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class CanvasLog : MonoBehaviour
{
    public static TextMeshProUGUI m_TextMeshProUGUI;
    public static TextMeshPro m_TextMeshPro;
    public static StringBuilder m_Text = new StringBuilder();

    private void Awake()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public static void AddText(string text)
    {
        m_Text.Append(text);
        m_TextMeshProUGUI.text = m_Text.ToString();
    }
    public static void AddText(object text)
    {
        m_Text.Append(text.ToString());
        m_TextMeshProUGUI.text = m_Text.ToString();
    }
    

    public static void AddTextSpace(string text)
    {
        m_Text.Append(text + " ");
        m_TextMeshProUGUI.text = m_Text.ToString();
    }
    public static void AddTextSpace(object text)
    {
        m_Text.Append(text.ToString() + " ");
        m_TextMeshProUGUI.text = m_Text.ToString();
    }

    public static void AddTextLine(string text)
    {
        m_Text.Append(text);
        m_Text.AppendLine();
        m_TextMeshProUGUI.text = m_Text.ToString();
    }
    public static void AddTextLine(object text)
    {
        m_Text.Append(text.ToString());
        m_Text.AppendLine();
        m_TextMeshProUGUI.text = m_Text.ToString();
    }

    public static void AddLine()
    {
        m_Text.AppendLine();
        m_TextMeshProUGUI.text = m_Text.ToString();
    }

    public void DeleteAllText()
    {
        m_Text.Clear();
        m_TextMeshProUGUI.text = m_Text.ToString();
    }
}
