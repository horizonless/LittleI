using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LevelFiveController : MonoBehaviour
{
    public Text displayText; // 用于显示消息的 UI 文本组件
    private string desktopPath;
    private string[] fileNames = { "隧道", "谱写", "怀疑", "答案" };
    public string[] initialContents = new string[4]; // 初始内容数组
    private float checkInterval = 5f; // 检查文件的间隔时间（秒）
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
        Screen.fullScreen = false; // 设置游戏为窗口化模式
    }

    void CreateAndInitializeFiles()
    {
        for (int i = 0; i < fileNames.Length; i++)
        {
            string filePath = Path.Combine(desktopPath, fileNames[i] + ".txt");
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, $"这是文件{fileNames[i]}的内容【在这里输入】");
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

            // 检查文件是否存在
            if (!File.Exists(filePath))
            {
                // 文件不存在，创建并初始化文件
                File.WriteAllText(filePath, initialContents[i]);
                continue; // 跳过当前循环的剩余部分
            }

            string currentContent = File.ReadAllText(filePath);

            // 检查【】外的内容是否被非法修改
            if (!IsOnlyBracketContentChanged(currentContent, initialContents[i]))
            {
                // 如果【】外的内容被修改，显示警告并重置文件
                ShowMessageOnScreen("禁止随意修改，请在空白处填入答案。修改后请关闭文件");
                File.WriteAllText(filePath, initialContents[i]);
            }
            else if (IsBracketContentFilled(currentContent))
            {
                // 如果【】内的内容被填写，更新 initialContents 以保存玩家的输入
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
        // 移除【】内的内容
        string pattern = @"【.*?】";
        string currentStripped = Regex.Replace(currentContent, pattern, "【】");
        string originalStripped = Regex.Replace(originalContent, pattern, "【】");

        // 比较移除【】内容后的原始和当前内容
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
        Screen.fullScreen = true; // 恢复全屏模式
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
        int startIndex = content.IndexOf('【');
        int endIndex = content.IndexOf('】');

        if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
        {
            return content.Substring(startIndex + 1, endIndex - startIndex - 1);
        }
        return "";
    }
}
