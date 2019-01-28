# UtilCore
基于 .NET Standard 2.0 开发的工具方法类库

# 平台支持
.NET Framework 4.6 和 .NET Core 2.0 以上项目

# 使用方法
   ITimerServiceFactory factory = new TimerServiceFactory();
   ITaskScheduler task = factory.GetScheduler("工作任务所在的项目名称","任务触发条件的配置文件名称").Result;
   await task.Start();
   
# 配置文件
  工作任务触发条件的配置文件。
  默认加载项目路径下 TaskFire.config 文件。
  设置文件为始终复制：右键该文件属性窗口中“复制到输出目录”选项中选择“始终复制”。
  Crontab 和 CycleTask 节配置一个即可，如果两个都设置了则默认以 Crontab 为任务触发条件。
  默认执行间隔时间为 1 天。
  没有设置 SpecifyTaskConfig 节点时，表示在每天的 FireTime 时间执行任务。 

<!-- 任务计划配置表 -->
<TaskScheduleConfig>
  <FireTime></FireTime>
  <SpecifyTaskConfig>
    <TaskTrigger>
      <Name></Name>
      <Description></Description>
      <Crontab>
        <FireTime></FireTime>
        <Interval></Interval>
      </Crontab>
      <CycleTask>
        <RepeatCount></RepeatCount>
        <Interval></Interval>
      </CycleTask>
    </TaskTrigger>
  </SpecifyTaskConfig>
</TaskScheduleConfig>
