<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="WcfService3.Azure" generation="1" functional="0" release="0" Id="68177459-694b-4f2d-b8af-8cb8508577da" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="WcfService3.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="WcfService3:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/WcfService3.Azure/WcfService3.AzureGroup/LB:WcfService3:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="WcfService3:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/WcfService3.Azure/WcfService3.AzureGroup/MapWcfService3:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WcfService3Instances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/WcfService3.Azure/WcfService3.AzureGroup/MapWcfService3Instances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:WcfService3:Endpoint1">
          <toPorts>
            <inPortMoniker name="/WcfService3.Azure/WcfService3.AzureGroup/WcfService3/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapWcfService3:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/WcfService3.Azure/WcfService3.AzureGroup/WcfService3/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWcfService3Instances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/WcfService3.Azure/WcfService3.AzureGroup/WcfService3Instances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="WcfService3" generation="1" functional="0" release="0" software="c:\users\sai prajnan\documents\visual studio 2013\Projects\WcfService3\WcfService3.Azure\csx\Release\roles\WcfService3" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WcfService3&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;WcfService3&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/WcfService3.Azure/WcfService3.AzureGroup/WcfService3Instances" />
            <sCSPolicyUpdateDomainMoniker name="/WcfService3.Azure/WcfService3.AzureGroup/WcfService3UpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/WcfService3.Azure/WcfService3.AzureGroup/WcfService3FaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="WcfService3UpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="WcfService3FaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="WcfService3Instances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="024d00e7-38f0-43f8-b4be-ebe2cee5a40a" ref="Microsoft.RedDog.Contract\ServiceContract\WcfService3.AzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="ac4aa85f-f264-4d5d-9d63-87d33e8dff4f" ref="Microsoft.RedDog.Contract\Interface\WcfService3:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/WcfService3.Azure/WcfService3.AzureGroup/WcfService3:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>