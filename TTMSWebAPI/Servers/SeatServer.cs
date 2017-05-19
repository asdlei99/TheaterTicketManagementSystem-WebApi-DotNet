using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TTMSWebAPI.Models;

namespace TTMSWebAPI.Servers
{
    /// <summary>
    /// 座位管理Server
    /// </summary>
    public class SeatServer
    {
        /// <summary>
        /// 获得所有座位
        /// </summary>
        /// <returns>座位列表</returns>
        public static object GetAllSeat()
        {
            using (var con = new SqlConnection(Server.SqlConString))
            {
                con.Open();

                var message = "";

                var sqlCom = new SqlCommand("sp_GetAllSeat", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCom.Parameters.AddRange(new[]
                {
                    new SqlParameter
                    {
                        ParameterName = "@message",
                        Direction = ParameterDirection.Output,
                        Size = 30,
                        SqlDbType = SqlDbType.VarChar,
                        Value = message
                    },
                    new SqlParameter
                    {
                        ParameterName = "@return",
                        Direction = ParameterDirection.ReturnValue,
                        SqlDbType = SqlDbType.Int
                    }
                });

                sqlCom.ExecuteNonQuery();

                var msg = (string) sqlCom.Parameters["@message"].Value;

                var data = new List<object>();

                var reader = sqlCom.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new
                    {
                        seatId = (int) reader[0],
                        theaterId = (int) reader[1],
                        status = (bool) reader[2],
                        seatRowNumber = (int) reader[3],
                        seatColNumber = (int) reader[4]
                    });
                }

                return new
                {
                    result = (int) sqlCom.Parameters["@return"].Value,
                    msg,
                    data
                };
            }
        }

        /// <summary>
        /// 查询座位信息
        /// </summary>
        /// <param name="seatId">座位ID</param>
        /// <returns>座位信息</returns>
        public static object QuerySeat(int seatId)
        {
            using (var con = new SqlConnection(Server.SqlConString))
            {
                con.Open();

                var message = "";

                var sqlCom = new SqlCommand("sp_QuerySeat", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCom.Parameters.AddRange(new[]
                {
                    new SqlParameter
                    {
                        ParameterName = "@seatId",
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.Int ,
                        Value = seatId
                    },
                    new SqlParameter
                    {
                        ParameterName = "@message",
                        Direction = ParameterDirection.Output,
                        Size = 30,
                        SqlDbType = SqlDbType.VarChar,
                        Value = message
                    },
                    new SqlParameter
                    {
                        ParameterName = "@return",
                        Direction = ParameterDirection.ReturnValue,
                        SqlDbType = SqlDbType.Int
                    }
                });

                sqlCom.ExecuteNonQuery();

                var msg = (string) sqlCom.Parameters["@message"].Value;

                var data = new List<object>();

                var reader = sqlCom.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new
                    {
                        seatId = (int) reader[0],
                        theaterId = (int) reader[1],
                        status = (bool) reader[2],
                        seatRowNumber = (int) reader[3],
                        seatColNumber = (int) reader[4]
                    });
                }

                return new
                {
                    result = (int) sqlCom.Parameters["@return"].Value,
                    msg,
                    data
                };
            }
        }

        /// <summary>
        /// 更新座位状态
        /// </summary>
        /// <param name="um">更新座位状态模型</param>
        /// <returns>更新结果</returns>
        public static object UpdateSeatStatus(UpdateSeatStatusModel um)
        {
            using (var con = new SqlConnection(Server.SqlConString))
            {
                con.Open();

                var sqlCom = new SqlCommand("sp_UpdateSeatStatus", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                sqlCom.Parameters.AddRange(new []
                {
                    new SqlParameter
                    {
                        ParameterName = "@seatId",
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.Int,
                        Value = um.SeatId
                    },
                    new SqlParameter
                    {
                        ParameterName = "@status",
                        Direction = ParameterDirection.Input,
                        SqlDbType = SqlDbType.Bit,
                        Value = um.Status
                    },
                    new SqlParameter
                    {
                        ParameterName = "@message",
                        Direction = ParameterDirection.Output,
                        Size = 30,
                        SqlDbType = SqlDbType.VarChar
                    },
                    new SqlParameter
                    {
                        ParameterName = "@return",
                        Direction = ParameterDirection.ReturnValue,
                        SqlDbType = SqlDbType.Int
                    }
                });

                sqlCom.ExecuteNonQuery();

                return new
                {
                    result = (int) sqlCom.Parameters["@return"].Value,
                    msg = (string) sqlCom.Parameters["@message"].Value
                };
            }
        }
    }
}