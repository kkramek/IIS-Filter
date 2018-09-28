<%@ Import Namespace="Filter" %>

<script language="C#" runat=server >
protected void Filter_OnMyEvent(Object src, EventArgs e)
{
  Context.Response.Write("Hello from Filter_OnMyEvent called in Global.asax.<br>");
}
</script>