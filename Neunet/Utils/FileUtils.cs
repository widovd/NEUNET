using System;
using System.IO;
using System.Windows.Forms;
using Neulib.Exceptions;
using Neunet.Forms;

namespace Neunet.Utils
{

    public static class FileUtils
    {
        /// <summary>
        /// Returns the index of the extension of a filename from a filter string.
        /// </summary>
        /// <param name="filter">The file filter string.</param>
        /// <param name="fileName">The file name with extension.</param>
        /// <returns>The index of the extension.</returns>
        public static int GetFilterIndex(string filter, string fileName)
        {
            // Example Filter:
            // CSV files (*.csv)|*.csv|TXT files (*.txt)|*.txt|All files (*.*)|*.*
            // Image Files (*.bmp, *.jpg)|*.bmp;*.jpg
            string extension = Path.GetExtension(fileName).ToLower(); // example: .txt
            string[] p1 = filter.Split('|');
            for (int i = 1; i < p1.Length; i += 2)
            {
                string[] p2 = p1[i].Split(';');
                for (int j = 0; j < p2.Length; j++)
                {
                    string a = p2[j].Trim().Substring(1).ToLower();
                    if (a == extension)
                        return i / 2 + 1;
                }
            }
            return 1;
        }

        /// <summary>
        /// Shows an OpenFileDialog.
        /// </summary>
        /// <param name="filePath">Input/Output: file name and initial directory.</param>
        /// <param name="filter">File extension filter.</param>
        /// <param name="filterIndex">Default filter index.</param>
        /// <returns>DialogResult.OK if the user selected a file.</returns>
        public static bool GetOpenFileName(ref string filePath, string filter, int filterIndex = 0)
        {
            bool result = false;
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = filter;

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        openFileDialog.InitialDirectory = Path.GetDirectoryName(filePath);
                        openFileDialog.FileName = Path.GetFileNameWithoutExtension(filePath);
                        openFileDialog.FilterIndex = filterIndex > 0 ? filterIndex : GetFilterIndex(filter, filePath);
                    }
                    else
                    {
                        openFileDialog.FilterIndex = filterIndex > 0 ? filterIndex : 1;
                    }
                    openFileDialog.RestoreDirectory = true;
                    result = openFileDialog.ShowDialog() == DialogResult.OK;
                    if (result) filePath = openFileDialog.FileName;
                }
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
            return result;
        }

        /// <summary>
        /// Shows a SaveFileDialog.
        /// </summary>
        /// <param name="filePath">Input/Output: file name and initial directory.</param>
        /// <param name="filter">File extension filter.</param>
        /// <param name="filterIndex">Default filter index.</param>
        /// <returns>DialogResult.OK if the user selected a file.</returns>
        public static bool GetSaveFileName(ref string filePath, string filter, int filterIndex = 0)
        {
            bool result = false;
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = filter;
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        saveFileDialog.InitialDirectory = Path.GetDirectoryName(filePath);
                        saveFileDialog.FileName = Path.GetFileNameWithoutExtension(filePath);
                        saveFileDialog.FilterIndex = filterIndex > 0 ? filterIndex : GetFilterIndex(filter, filePath);
                    }
                    else
                    {
                        saveFileDialog.FilterIndex = filterIndex > 0 ? filterIndex : 1;
                    }
                    saveFileDialog.RestoreDirectory = true;
                    result = saveFileDialog.ShowDialog() == DialogResult.OK;
                    if (result) filePath = saveFileDialog.FileName;
                }
            }
            catch (BaseException ex)
            {
                ExceptionDialog.Show(ex);
            }
            return result;
        }

    }
}
