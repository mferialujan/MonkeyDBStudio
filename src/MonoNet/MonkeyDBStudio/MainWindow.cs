using System;
using System.Diagnostics;
using Gtk;
using java.sql;

public partial class MainWindow : Gtk.Window
{
	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void OnButton1Clicked(object sender, EventArgs e)
	{
		StartConnection();
	}
	private void StartConnection()
	{
		string sql = "SELECT  * from tblusrusuarios";

		PreparedStatement preparedStatement = null;
		Statement stmt;
		Connection conn = null;

		try
		{
			DriverManager.registerDriver(new com.informix.jdbc.IfxDriver());
			conn = DriverManager.getConnection("jdbc:informix-sqli://servermachineorip:port/database:informixserver=server", "user", "password");
			conn.setAutoCommit(false);
			//preparedStatement = conn.prepareStatement(sql, ResultSet.__Fields.TYPE_SCROLL_INSENSITIVE, ResultSet.__Fields.CONCUR_READ_ONLY);
			stmt = conn.createStatement(ResultSet.__Fields.TYPE_SCROLL_INSENSITIVE, ResultSet.__Fields.CONCUR_READ_ONLY);
			stmt.setFetchSize(100);
			//preparedStatement.setInt(1, 1);

			bool results = stmt.execute(sql); //preparedStatement.execute();
			//bool results = preparedStatement.executeQuery(); //preparedStatement.execute();
			int rsCount = 0;
			//DateTime hora = new DateTime();
			button1.Label = DateTime.Now.ToString();
			while (results)
			{
				//stmt.getMoreResults();
				ResultSet rs = stmt.getResultSet();
				ResultSetMetaData rsmd = rs.getMetaData();
				rsCount++;

				//string res;
				while (rs.next())
				{
					object o=rs.getObject(10);

					Date d= rs.getDate("fechacreacion");
					/*if (rsmd.getCatalogName(0).ToUpper() == "SI_CODRET")
						res = rs.getString("CODIGO_RETORNO");
					else
						res =rs.getString("SISTEMA");*/
				}
				results = stmt.getMoreResults();
			}
			button1.Label = button1.Label+"---"+DateTime.Now.ToString();
			Debug.Print("Final :" + DateTime.Now.ToString());
			//ConnectionHandler ch = new ConnectionHandler();
			//ch.setConnection(conn);

			return;
		}
		catch (Exception ex)
		{
			if(conn is com.informix.jdbc.IfxConnection)
			{
				int offset=(conn as com.informix.jdbc.IfxConnection).getSQLStatementOffset();
			}

			//throw ex;
		}

	}
}
