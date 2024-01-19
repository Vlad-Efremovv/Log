Хэширование верез MD5

код :
    
public static string CalculateMD5Hash(string input)
{
    using (MD5 md5 = MD5.Create())
    {
        byte[] inputBytes = Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("x2"));
        }
        return sb.ToString();
    }
}


SELECT 
    Прием.Код AS КодПриема,
    Врачь.ФИО AS ФИОВрача,
    Пациент.ФИО AS ФИОПациента,
    Прием.ДатаВизита,
    Стоимость.Сумма AS СтоимостьВизита,
    Диагноз.Наименование AS НаименованиеДиагноза
FROM 
    Прием
JOIN 
    Врачь ON Прием.КодВрача = Врачь.Код
JOIN 
    Пациент ON Прием.КодПациента = Пациент.НомерКарты
JOIN 
    Стоимость ON Прием.КодСтоимости = Стоимость.Код
LEFT JOIN 
    Диагноз ON Прием.КодДиагноза = Диагноз.Код;
