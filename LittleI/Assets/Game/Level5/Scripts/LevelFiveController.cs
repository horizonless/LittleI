using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LevelFiveController : MonoBehaviour
{
    public Text displayText; // ������ʾ��Ϣ�� UI �ı����
    private string desktopPath;
    private string[] fileNames = { "���", "��д", "����", "��" };
    public string[] initialContents = new string[4]; // ��ʼ��������
    private float checkInterval = 5f; // ����ļ��ļ��ʱ�䣨�룩
    private bool isFullScreenRestored = false;

    void Start()
    {
        desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        CreateAndInitializeFiles();
        StartCoroutine(CheckFilesRoutine());
        MakeGameWindowed();
    }

    void MakeGameWindowed()
    {
        Screen.fullScreen = false; // ������ϷΪ���ڻ�ģʽ
    }

    void CreateAndInitializeFiles()
    {
        for (int i = 0; i < fileNames.Length; i++)
        {
            string filePath = Path.Combine(desktopPath, fileNames[i] + ".txt");
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, $"�����ļ�{fileNames[i]}�����ݡ����������롿");
            }
        }
    }

    IEnumerator CheckFilesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);
            CheckFiles();

            if (CheckAllFilesFilled() && !isFullScreenRestored)
            {
                RestoreFullScreenAndDisplayMessage();
                isFullScreenRestored = true;
            }
        }
    }

    void CheckFiles()
    {
        for (int i = 0; i < fileNames.Length; i++)
        {
            string filePath = Path.Combine(desktopPath, fileNames[i] + ".txt");

            // ����ļ��Ƿ����
            if (!File.Exists(filePath))
            {
                // �ļ������ڣ���������ʼ���ļ�
                File.WriteAllText(filePath, initialContents[i]);
                continue; // ������ǰѭ����ʣ�ಿ��
            }

            string currentContent = File.ReadAllText(filePath);

            // ��顾����������Ƿ񱻷Ƿ��޸�
            if (!IsOnlyBracketContentChanged(currentContent, initialContents[i]))
            {
                // �������������ݱ��޸ģ���ʾ���沢�����ļ�
                ShowMessageOnScreen("��ֹ�����޸ģ����ڿհ״�����𰸡��޸ĺ���ر��ļ�");
                File.WriteAllText(filePath, initialContents[i]);
            }
            else if (IsBracketContentFilled(currentContent))
            {
                // ��������ڵ����ݱ���д������ initialContents �Ա�����ҵ�����
                initialContents[i] = currentContent;
            }
        }
    }


    bool CheckAllFilesFilled()
    {
        foreach (string fileName in fileNames)
        {
            string filePath = Path.Combine(desktopPath, fileName + ".txt");
            string content = File.ReadAllText(filePath);
            if (!IsBracketContentFilled(content))
            {
                return false;
            }
        }
        return true;
    }

    bool IsOnlyBracketContentChanged(string currentContent, string originalContent)
    {
        // �Ƴ������ڵ�����
        string pattern = @"��.*?��";
        string currentStripped = Regex.Replace(currentContent, pattern, "����");
        string originalStripped = Regex.Replace(originalContent, pattern, "����");

        // �Ƚ��Ƴ��������ݺ��ԭʼ�͵�ǰ����
        return currentStripped == originalStripped;
    }

    bool IsBracketContentFilled(string content)
    {
        string bracketContent = ExtractBracketContent(content);
        return !string.IsNullOrEmpty(bracketContent) && bracketContent.Trim().Length > 0;
    }

    void ShowMessageOnScreen(string message)
    {
        displayText.text = message;
    }

    void RestoreFullScreenAndDisplayMessage()
    {
        Screen.fullScreen = true; // �ָ�ȫ��ģʽ
        string message = "";

        foreach (string fileName in fileNames)
        {
            string filePath = Path.Combine(desktopPath, fileName + ".txt");
            string content = ExtractBracketContent(File.ReadAllText(filePath));
            message += content + " ";
        }

        ShowMessageOnScreen(message.TrimEnd());
    }

    string ExtractBracketContent(string content)
    {
        int startIndex = content.IndexOf('��');
        int endIndex = content.IndexOf('��');

        if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
        {
            return content.Substring(startIndex + 1, endIndex - startIndex - 1);
        }
        return "";
    }
}
