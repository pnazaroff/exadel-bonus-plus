using System;
using System.Collections.Generic;

namespace ExadelBonusPlus.DataAccess
{
    public interface IGenericCrud
    {
        //Create 
        void InsertRecord<T>(string collectionName, T record);
        //Read All 
        List<T> LoadRecords<T>(string collectionName);
        //Read by Id
        T LoadRecordById<T>(string collectionName, Guid id);
        //Upsert
        /// <summary>
        /// Inserts or updates where it is necessary 
        /// if record is available update it
        /// Otherwise, create one. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">Name of the collection in Db</param>
        /// <param name="id">Id of the record</param>
        /// <param name="record">Full record of the document</param>
        void UpsertRecord<T>(string collectionName, Guid id, T record);
        void DeleteRecord<T>(string collectionName, Guid id); 

    }
}
