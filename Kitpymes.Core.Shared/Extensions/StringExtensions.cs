// -----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Kitpymes">
// Copyright (c) Kitpymes. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project docs folder for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Kitpymes.Core.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Net.Mime;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.StaticFiles;

    /*
      Clase de extensión StringExtensions
      Contiene las extensiones de las cadenas string
    */

    /// <summary>
    /// Clase de extensión <c>StringExtensions</c>.
    /// Contiene las extensiones de las cadenas string.
    /// </summary>
    /// <remarks>
    /// <para>En esta clase se pueden agregar todas las extensiones para las cadenas string.</para>
    /// </remarks>
    public static class StringExtensions
    {
        #region ToRemove

        /// <summary>
        /// Caracteres que se requieren remover.
        /// </summary>
        /// <param name="input">Cadena a remplazar.</param>
        /// <param name="removes">Caracteres que se quieren remover.</param>
        /// <returns>La cadena con los caracteres removidos.</returns>
        public static string? ToRemove(this string? input, params string[] removes)
        {
            if (input.ToIsNullOrEmpty() || removes.ToIsNullOrAny())
            {
                return input;
            }

            foreach (var remove in removes)
            {
                input = input?.Replace(remove, string.Empty, StringComparison.CurrentCulture);
            }

            return input;
        }

        #endregion ToRemove

        #region ToReplace

        /// <summary>
        /// Reemplaza el valor de una cadena por otro.
        /// </summary>
        /// <param name="input">Cadena que contiene el valor a reemplazar.</param>
        /// <param name="replace">Valor con que se quieren reemplazar.</param>
        /// <param name="start">Posición inicial de donde se encuentra el valor a ser reemplazado.</param>
        /// <param name="count">Cantidad de caracteres a ser reemplazado.</param>
        /// <returns>La cadena con el valor reemplazado.</returns>
        public static string? ToReplace(this string? input, string replace, int start, int count)
        {
            if (input.ToIsNullOrEmpty() || replace.ToIsNullOrEmpty())
            {
                return input;
            }

            if (start + count > input?.Length)
            {
                start = 0;
                count = input.Length;
            }

            var length = start + count;

            for (int i = start; i < length; i++)
            {
                input = input?.Remove(i, 1).Insert(i, replace);
            }

            return new string(input);
        }

        /// <summary>
        /// Reemplaza caracteres especiales o los reemplaza por un espacio vacio.
        /// </summary>
        /// <param name="input">El valor a reemplazar.</param>
        /// <param name="ignoreSpecialChars">Los caracteres que no queremos reemplazar.</param>
        /// <returns>La cadena sin caracteres especiales.</returns>
        public static string? ToReplaceSpecialChars(this string? input, params string[] ignoreSpecialChars)
        {
            if (input.ToIsNullOrEmpty())
            {
                return input;
            }

            var dictionary = new Dictionary<char, char[]>
            {
                { 'a', new[] { 'à', 'á', 'ä', 'â', 'ã' } },
                { 'A', new[] { 'À', 'Á', 'Ä', 'Â', 'Ã', 'Å' } },
                { 'c', new[] { 'ç' } },
                { 'C', new[] { 'Ç' } },
                { 'e', new[] { 'è', 'é', 'ë', 'ê' } },
                { 'E', new[] { 'È', 'É', 'Ë', 'Ê' } },
                { 'i', new[] { 'ì', 'í', 'ï', 'î' } },
                { 'I', new[] { 'Ì', 'Í', 'Ï', 'Î' } },
                { 'o', new[] { 'ò', 'ó', 'ö', 'ô', 'õ', 'ø' } },
                { 'O', new[] { 'Ò', 'Ó', 'Ö', 'Ô', 'Õ' } },
                { 'u', new[] { 'ù', 'ú', 'ü', 'û' } },
                { 'U', new[] { 'Ù', 'Ú', 'Ü', 'Û' } },
                { 'h', new[] { 'ħ' } },
                { 'n', new[] { 'ñ' } },
                { 'N', new[] { 'Ñ' } },
            };

            var pattern = "[^0-9a-zA-Z ";

            if (ignoreSpecialChars?.Length > 0)
            {
                foreach (string c in ignoreSpecialChars)
                {
                    pattern += c;
                }
            }

            pattern += "]+?";

            input = dictionary.Keys.Aggregate(input, (x, y) => dictionary[y].Aggregate(x, (z, c) => z?.Replace(c, y)));

            return new Regex(pattern).Replace(input, string.Empty);
        }

        #endregion ToReplace

        #region ToNormalize

        /// <summary>
        /// Normaliza una cadena removiendo los caracteres especiales y reemplanzando los caracteres con acento.
        /// </summary>
        /// <param name="input">Cadena a normalizar.</param>
        /// <returns>Cadena normalizada.</returns>
        public static string? ToNormalize(this string? input)
        => input.ToReplaceSpecialChars()?.Trim();

        #endregion ToReplaceOrRemoveOrNormalize

        #region ToEnum

        /// <summary>
        /// Convierte una cadena de tipo {TEnum} en un enum.
        /// </summary>
        /// <typeparam name="TEnum">Tipo de enum.</typeparam>
        /// <param name="input">Cadena del enum.</param>
        /// <param name="defaultValue">Valor por defecto que se le asigna si el valor no pertenece al tipo {TEnum}.</param>
        /// <returns>Enum de tipo {TEnum}.</returns>
        public static TEnum ToEnum<TEnum>(this string? input, TEnum defaultValue = default)
            where TEnum : struct, Enum
            => input.ToIsNullOrEmpty() || !Enum.TryParse(input, false, out TEnum result) ? defaultValue : result;

        /// <summary>
        /// Convierte un entero de tipo {TEnum} en un enum.
        /// </summary>
        /// <typeparam name="TEnum">Tipo de enum.</typeparam>
        /// <param name="input">Valor del enum.</param>
        /// <param name="defaultValue">Valor por defecto que se le asigna si el valor no pertenece al tipo {TEnum}.</param>
        /// <returns>Enum de tipo {TEnum}.</returns>
        public static TEnum ToEnum<TEnum>(this int input, TEnum defaultValue = default)
            where TEnum : struct, Enum
          => Enum.GetName(typeof(TEnum), input).ToEnum(defaultValue);

        #endregion ToEnum

        #region ToFirstLetter

        /// <summary>
        /// Convierte la primer letra de una cadena en mayúscula.
        /// </summary>
        /// <param name="input">Cadena a convertir.</param>
        /// <returns>Cadena convertida.</returns>
        public static string? ToFirstLetterUpper(this string input)
        => string.IsNullOrWhiteSpace(input) ? input : char.ToUpperInvariant(input[0]) + input.Substring(1);

        /// <summary>
        /// Convierte la primer letra de una cadena en minúscula.
        /// </summary>
        /// <param name="input">Cadena a convertir.</param>
        /// <returns>Cadena convertida.</returns>
        public static string? ToFirstLetterLower(this string input)
        => string.IsNullOrWhiteSpace(input) ? input : char.ToLowerInvariant(input[0]) + input.Substring(1);

        #endregion ToFirstLetter

        #region ToEmailMask

        /// <summary>
        /// Crea una máscara en un email.
        /// </summary>
        /// <param name="input">Email a crear máscara.</param>
        /// <param name="replace">Caracter con que queremos crear la máscara.</param>
        /// <returns>miemail@gmail.com => m*****l@gmail.com.</returns>
        public static string? ToEmailMaskUserName(this string? input, char replace = '*')
            => input.ToEmailMask(@"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)", replace);

        /// <summary>
        /// Crea una máscara en un email.
        /// </summary>
        /// <param name="input">Email a crear máscara.</param>
        /// <param name="replace">Caracter con que queremos crear la máscara.</param>
        /// <returns>miemail@gmail.com => m*****l@g***l.com.</returns>
        public static string? ToEmailMaskUserNameAndDomain(this string? input, char replace = '*')
            => input.ToEmailMask(@"(?<=(?:^|@)[^.]*)\B.\B", replace);

        /// <summary>
        /// Crea una máscara en un email.
        /// </summary>
        /// <param name="input">Email a crear máscara.</param>
        /// <param name="replace">Caracter con que queremos crear la máscara.</param>
        /// <returns>// miemail@gmail.com => m*****l@g***l.c*m.</returns>
        public static string? ToEmailMaskUserNameAndDomainAndExtension(this string? input, char replace = '*')
            => input.ToEmailMask(@"(?<=(?:^|@)[^.]*)\B.\B|(?<=[\w]{1})[\w-\+%]*(?=[\w]{1})", replace);

        /// <summary>
        /// Crea una máscara en un email.
        /// </summary>
        /// <param name="input">Email a crear máscara.</param>
        /// <param name="pattern">Expresión regular.</param>
        /// <param name="replace">Caracter con que queremos crear la máscara.</param>
        /// <returns>El email con las máscara aplicada.</returns>
        public static string? ToEmailMask(this string? input, string pattern, char replace)
            => string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(pattern) || replace == default ? input
            : Regex.Replace(input, pattern, m => new string(replace, m.Length));

        #endregion ToEmailMask

        #region ToUri

        /// <summary>
        /// Convierte una cadena en una Uri.
        /// </summary>
        /// <param name="input">Cadena a convertir.</param>
        /// <param name="defaultValue">Valor por defecto.</param>
        /// <returns>Uri | null | defaultValue.</returns>
        public static Uri? ToUri(this string? input, Uri? defaultValue = null)
        => !string.IsNullOrWhiteSpace(input)
                && Uri.TryCreate(input, UriKind.RelativeOrAbsolute, out var result) ? result : defaultValue;

        #endregion ToUri

        #region ToHashAlgorithmName

        /// <summary>
        /// Obtiene el nombre del algoritmo.
        /// </summary>
        /// <param name="input">Cadena a evaluar.</param>
        /// <param name="defaultValue">Valor por defecto.</param>
        /// <returns>HashAlgorithmName 1 null.</returns>
        public static HashAlgorithmName? ToHashAlgorithmName(this string? input, HashAlgorithmName? defaultValue = null)
        {
            return input switch
            {
                string _ when HashAlgorithmName.MD5.ToString() == input => HashAlgorithmName.MD5,
                string _ when HashAlgorithmName.SHA1.ToString() == input => HashAlgorithmName.SHA1,
                string _ when HashAlgorithmName.SHA256.ToString() == input => HashAlgorithmName.SHA256,
                string _ when HashAlgorithmName.SHA384.ToString() == input => HashAlgorithmName.SHA384,
                string _ when HashAlgorithmName.SHA512.ToString() == input => HashAlgorithmName.SHA512,

                _ => defaultValue,
            };
        }

        #endregion ToHashAlgorithmName

        #region ToInt

        /// <summary>
        /// Convierte una cadena en número entero.
        /// </summary>
        /// <param name="input">Valor a convertir.</param>
        /// <param name="defaultValue">Valor por defecto.</param>
        /// <returns>int | null | defaultValue.</returns>
        public static int? ToInt(this string? input, int? defaultValue = null)
        {
            if (int.TryParse(input, out var i))
            {
                return i;
            }

            return defaultValue;
        }

        #endregion ToInt

        #region ToZip

        /// <summary>
        /// Comprime el contenido de una carpeta.
        /// </summary>
        /// <param name="sourceDirectoryPath">Path del directorio que queremos comprimir.</param>
        /// <param name="destinationDirectoryPath">Path del directorio donde queremos guardar el archivo comprimido.</param>
        /// <param name="customZipName">Nombre del archivo ZIP opcional.</param>
        /// <param name="compressionLevel">Tipo de compresión.</param>
        /// <param name="removeSourceDirectoryPath">Si queremos remover el directorio.</param>
        public static void ToZipCreate(
            this string? sourceDirectoryPath,
            string? destinationDirectoryPath,
            string? customZipName = null,
            CompressionLevel compressionLevel = CompressionLevel.Optimal,
            bool removeSourceDirectoryPath = false)
        {
            customZipName = string.IsNullOrWhiteSpace(customZipName) ? sourceDirectoryPath?.Split("\\").Last() : customZipName;

            ZipFile.CreateFromDirectory(sourceDirectoryPath, $"{destinationDirectoryPath}/{customZipName}.zip", compressionLevel, false);

            if (removeSourceDirectoryPath)
            {
                Directory.Delete(sourceDirectoryPath, true);
            }
        }

        /// <summary>
        /// Comprime el contenido de una carpeta.
        /// </summary>
        /// <param name="sourceFilePath">Path del archivo ZIP que queremos extraer.</param>
        /// <param name="destinationDirectoryPath">Path del directorio donde queremos guardar los archivos.</param>
        /// <param name="overwriteFiles">Sobreescribir los archivos.</param>
        public static void ToZipExtract(
            this string? sourceFilePath,
            string? destinationDirectoryPath,
            bool overwriteFiles = false)
        => ZipFile.ExtractToDirectory(sourceFilePath, destinationDirectoryPath, overwriteFiles);

        #endregion ToZip

        #region ToContentType

        /// <summary>
        /// Obtiene el tipo de contenido de un archivo.
        /// </summary>
        /// <param name="fileName">Archivo.</param>
        /// <param name="defaultValue">Valor por defecto.</param>
        /// <returns>El tipo de contenido.</returns>
        public static string ToContentType(this string? fileName, string defaultValue = MediaTypeNames.Application.Octet)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = defaultValue;
            }

            return contentType;
        }

        #endregion ToContentType

        #region ToDirectory

        /// <summary>
        /// Obtiene el path de un archivo.
        /// </summary>
        /// <param name="directoryPath">Directorio donde se buscara el archivo.</param>
        /// <param name="fileName">Nombre del archivo a buscar.</param>
        /// <param name="ignoreCase">Si se ignora el camel case.</param>
        /// <returns>El path del archivo | null.</returns>
        public static string? ToDirectoryFindFilePath(this string? directoryPath, string fileName, bool ignoreCase = true)
        {
            if (string.IsNullOrWhiteSpace(directoryPath) || string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            foreach (var fullFile in Directory.GetFiles(directoryPath))
            {
                var file = Path.GetFileName(fullFile).Split(".")[0];

                if (ignoreCase)
                {
                    file = file.ToLower(CultureInfo.CurrentCulture);
                    fileName = fileName.ToLower(CultureInfo.CurrentCulture);
                }

                if (file == fileName)
                {
                    return fullFile;
                }
            }

            foreach (var path in Directory.GetDirectories(directoryPath))
            {
                var file = path.ToDirectoryFindFilePath(fileName);

                if (!string.IsNullOrEmpty(file))
                {
                    return file;
                }
            }

            return null;
        }

        /// <summary>
        /// Obtiene la información de un archivo.
        /// </summary>
        /// <param name="directoryPath">Ruta del directorio donde se buscara el archivo.</param>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <returns>FileInfo | null.</returns>
        public static FileInfo? ToDirectoryFileInfo(this string? directoryPath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(directoryPath) || string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            return new DirectoryInfo(directoryPath).GetFiles(string.Concat(fileName, ".", "*")).Single();
        }

        /// <summary>
        /// Elimina todas las filas de un directorio, y opcionalmente puede eliminar los subdirectorios y el directorio raiz.
        /// </summary>
        /// <param name="directoryPath">Ruta del directorio.</param>
        /// <param name="recursive">Si se quiere eliminar todos los subdirectorios.</param>
        /// <param name="directoryDelete">Si se quiere eliminar el directorio raiz.</param>
        public static void ToDirectoryDeleteFiles(this string directoryPath, bool recursive = false, bool directoryDelete = false)
        {
            if (!string.IsNullOrWhiteSpace(directoryPath))
            {
                foreach (var file in Directory.GetFiles(directoryPath))
                {
                    var attr = File.GetAttributes(file);

                    if ((attr & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        File.SetAttributes(file, attr ^ FileAttributes.ReadOnly);
                    }

                    File.Delete(file);
                }

                if (directoryDelete)
                {
                    Directory.Delete(directoryPath, recursive);
                }
            }
        }

        /// <summary>
        /// Guarda un archivo en un directorio, si el directorio no existe lo crea.
        /// </summary>
        /// <param name="directoryPath">Ruta del directorio.</param>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <param name="bytes">Bytes del archivo.</param>
        /// <param name="ifNoExistDirectoryCreate">Si no existe el directorio lo crea.</param>
        /// <returns>Task.</returns>
        public static async Task ToDirectorySaveFileAsync(this string directoryPath, string fileName, byte[] bytes, bool ifNoExistDirectoryCreate = true)
        {
            if (!string.IsNullOrWhiteSpace(directoryPath) && !string.IsNullOrWhiteSpace(fileName) && bytes != null && bytes.Length > 0)
            {
                if (ifNoExistDirectoryCreate && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var path = Path.Combine(directoryPath, fileName);

                await File.WriteAllBytesAsync(path, bytes).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Obtiene los datos de un archivo.
        /// </summary>
        /// <param name="directoryPath">Directorio donde se encuentra el archivo.</param>
        /// <param name="fileName">Nombre del archivo.</param>
        /// <returns>La información del archivo.</returns>
        public static async Task<(string fileName, string filePath, string fileConvert, byte[] fileBytes, long fileLength, string fileContentType)?> ToDirectoryReadFileAsync(
            this string directoryPath,
            string fileName)
        {
            if (!string.IsNullOrWhiteSpace(directoryPath) || !string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            var fileInfo = ToDirectoryFileInfo(directoryPath, fileName);

            if (fileInfo is null)
            {
                return null;
            }

            var filePath = fileInfo.FullName;

            var fileBytes = await File.ReadAllBytesAsync(filePath).ConfigureAwait(false);

            var fileConvert = Convert.ToBase64String(fileBytes);

            return (fileInfo.Name, filePath, fileConvert, fileBytes, fileInfo.Length, fileInfo.Extension);
        }

        /// <summary>
        /// Obtiene los datos de los archivos que se encuentran el directorio.
        /// </summary>
        /// <param name="directoryPath">Directorio donde se encuentran los archivos.</param>
        /// <param name="searchOption">Opciones de búsqueda.</param>
        /// <param name="includeExtensions">Extensiones a incluir, si no se pasa ningún valor incluye a todas.</param>
        /// <returns>La lista con los datos de los archivos.</returns>
        public static async Task<List<(string fileName, string filePath, string fileConvert, byte[] fileBytes, long fileLength, string fileContentType)?>?> ToDirectoryReadFilesAsync(
            this string directoryPath,
            SearchOption searchOption = SearchOption.AllDirectories,
            params string[] includeExtensions)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                return null;
            }

            List<(string fileName, string filePath, string fileConvert, byte[] fileBytes, long fileLength, string fileContentType)?> filesReading = new List<(string fileName, string filePath, string fileConvert, byte[] fileBytes, long fileLength, string fileContentType)?>();

            includeExtensions ??= new[] { "*.*" };

            foreach (var extension in includeExtensions)
            {
                foreach (var name in Directory.GetFiles(directoryPath, extension, searchOption))
                {
                    var file = await ToDirectoryReadFileAsync(directoryPath, name).ConfigureAwait(false);

                    filesReading.Add(file);
                }
            }

            return filesReading;
        }

        /// <summary>
        /// Crea una carpeta en el directorio de archivos temporales.
        /// </summary>
        /// <param name="folderName">Nombre de la carpeta.</param>
        /// <returns>La ruta comleta de la carpeta creada.</returns>
        public static string ToDirectoryTemporary(this string folderName)
        {
            var path = Path.Combine(Path.GetTempPath(), folderName);

            return Directory.CreateDirectory(path).FullName;
        }

        #endregion ToDirectory

        #region ToFormat

        /// <summary>
        /// Formatea un valor.
        /// </summary>
        /// <param name="value">El valor que se le quiere dar formato.</param>
        /// <param name="args">Los argumentos para el formato.</param>
        /// <returns>El valor formateado.</returns>
        public static string ToFormat(this string value, params object[] args)
        => value.ToFormat(CultureInfo.CurrentCulture, args);

        /// <summary>
        /// Formatea un valor.
        /// </summary>
        /// <param name="value">El valor que se le quiere dar formato.</param>
        /// <param name="formatProvider">El proveedor para dar formato.</param>
        /// <param name="args">Los argumentos para el formato.</param>
        /// <returns>El valor formateado.</returns>
        public static string ToFormat(this string value, IFormatProvider formatProvider, params object[] args)
        => string.Format(formatProvider, value, args);

        #endregion ToFormat

        #region ToAssembly

        /// <summary>
        /// Carga los assemblies por el nombre.
        /// </summary>
        /// <param name="assembly">Nombre del assembly a cargar.</param>
        /// <returns>Assembly.</returns>
        public static Assembly ToAssembly(this string assembly)
        => Assembly.Load(assembly);

        #endregion ToAssembly
    }
}