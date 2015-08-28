namespace CS.Serialization
{
    /// <summary>
    /// 命令类型
    /// <remarks>
    /// 大于10000的命令类型都是用Command里的参数组合后自定查询。不是动态拼凑的。
    /// </remarks>
    /// </summary>
    public enum CommandStyle
    {

        /// <summary>
        /// 1 调试模式
        /// </summary>
        Debug = 0,
        /// <summary>
        /// 1 普通命令，通过动态拼凑而成，所有页面全部自动渲染
        /// </summary>
        Dynamic = 1, 
        /// <summary>
        /// 2 自定义模式，这个会转到具体的页面进行执行，进行部分页面的渲染
        /// </summary>
        Custom = 2, 
        /// <summary>
        /// 3 报表模式，该模式下没有分页，直接导出结果
        /// </summary>
        Report = 3,

       

        /////************************************/////////////

        /// <summary>
        /// 210000 开发工具模式
        /// </summary>
        Tool = 210000,
        /// <summary>
        /// 220000 查询分析器
        /// </summary>
        QueryAnalyzer = 220000,
        /// <summary>
        /// 230000 权限管理模式
        /// </summary>
        Permission = 230000,

    }
}