# Easycode.TimerService
基于 .NET Standard 2.0 开发的定时任务类库

# 使用方法
   ITimerServiceFactory factory = new TimerServiceFactory(); <br />
   ITaskScheduler task = await factory.GetScheduler("工作任务所在的项目名称","任务触发条件的配置文件名称"); <br />
   await task.Start();
   
# 配置文件
  工作任务触发条件的配置文件，默认加载项目路径下 TaskFire.config 文件。<br />
  设置文件为始终复制：右键该文件属性窗口中“复制到输出目录”选项中选择“始终复制”。<br />
  Crontab 和 CycleTask 节配置一个即可，如果两个都设置了则默认以 Crontab 为任务触发条件。

```
<TaskScheduleConfig>
  <FireTime>全局执行时间(时分秒格式，如：00:00:00)</FireTime>
  <Interval>全局执行间隔时间(以“天”为单位，默认 1 天，可不设置)</Interval>
  SpecifyTaskConfig 指定任务配置节点，可不设置
  <SpecifyTaskConfig>
    <TaskTrigger>
      <Name>任务类名1</Name>
      <Description>任务描述</Description>
      <Crontab>
        <FireTime>执行时间(时分秒格式，如：00:00:00)</FireTime>
        <Interval>执行间隔时间(以“天”为单位)</Interval>
      </Crontab>
      <CycleTask>
        <RepeatCount>重复执行次数(-1 表示无限循环执行)</RepeatCount>
        <Interval>执行间隔时间(以“秒”为单位)</Interval>
      </CycleTask>
    </TaskTrigger>

  <TaskTrigger>
      <Name>任务类名2</Name>
      <Description>任务描述</Description>
      <Crontab>
        <FireTime>执行时间(时分秒格式，如：00:00:00)</FireTime>
        <Interval>执行间隔时间(以“天”为单位)</Interval>
      </Crontab>
      <CycleTask>
        <RepeatCount>重复执行次数(-1 表示无限循环执行)</RepeatCount>
        <Interval>执行间隔时间(以“秒”为单位)</Interval>
      </CycleTask>
  </TaskTrigger>

  </SpecifyTaskConfig>
</TaskScheduleConfig>
```