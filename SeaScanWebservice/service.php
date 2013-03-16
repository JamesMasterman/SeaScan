<?php
require 'Slim/Slim.php';
require 'DBManagement.php';
\Slim\Slim::registerAutoloader();

$app = new \Slim\Slim();

  
//Get specific location
$app->get("/locations/:user/:password/:id/:inuse/:targetfound", function($user, $password, $id, $inuse, $targetFound) use($app)
{
 $type = authenticate_user($user, $password);

 if(connect($type))
 { 
	 if(strtolower($id) == "all")
	 {
		if(strtolower($inuse) == "true")
		{
			
			if(strtolower($targetFound) == "true")
			{
				$sql = "SELECT * FROM locations WHERE ID in (Select LocationID from missions where TargetsDetected > 0)";
			}
			else
			{
				$sql = "SELECT * FROM locations where ID in (Select LocationID from missions)";
			}
			
		}
		else
		{
			$sql = "SELECT * FROM locations";
			if(strtolower($targetFound) == "true")
			{
				$sql = $sql." WHERE ID in (Select LocationID from missions where TargetsDetected > 0)";
			}			
		}				
	 }else
	 {	    
		$sql = "SELECT * FROM locations where ID = $id";
		if(strtolower($targetFound) == "true")
		{
			$sql = $sql." and ID in (Select LocationID from missions where TargetsDetected > 0)";
		}		
	 }
	 
	 $result = mysql_query($sql);
	 $locations = array();

	 while($row = mysql_fetch_array($result))
	 {
		 $locations[] = array("ID"=> $row['id'], 
							 "LocationName"=> $row['LocationName'], 
							 "MinX" => $row['MinX'], 
							 "MinY" => $row['MinY'], 
							 "MaxX"=>$row['MaxX'], 
							 "MaxY"=>$row['MaxY']);
	 }

	 $app->response()->header("Content-Type", "application/json");
	 echo json_encode($locations);
	 
	 close_connection();
 }else
 {
	$app->response()->status(401);
    $app->response()->header("Content-Type", "text/plain");
 }
});

//Get specific PilotID
$app->get("/pilot/:user/:password", function($user, $password) use($app)
{
$type = authenticate_user($user, $password);
 
 if(connect($type))
 { 
	 
	$result = mysql_query("SELECT * FROM users where UserName = '$user' and Password = '$password' and Enabled = 1");
	$pilot = array();

	 while($row = mysql_fetch_array($result))
	 {
		 $pilot[] = array("ID"=> $row['ID'],
						  "UserName"=> $row['UserName'],
						  "FirstName"=> $row['FirstName'],
						  "LastName"=> $row['LastName'],
						  "Description"=>$row['Description'],
						  "IsAdmin"=>$row['IsAdmin']);
	 }

	 $app->response()->header("Content-Type", "application/json");
	 echo json_encode($pilot);
	 
	 close_connection();
 }else
 {
	$app->response()->status(401);
	$app->response()->header("Content-Type", "text/plain");
 }
});

//Get airframes
$app->get("/airframes/:user/:password", function($user, $password) use($app)
{
$type = authenticate_user($user, $password);
 
 if(connect($type))
 { 
	 
	$result = mysql_query("SELECT * FROM aircraft");
	$aircraft = array();

	 while($row = mysql_fetch_array($result))
	 {
		 $aircraft[] = array("ID"=> $row['ID'],
							 "PlaneName"=> $row['PlaneName'],
							 "Description"=> $row['Description']);
	 }

	 $app->response()->header("Content-Type", "application/json");
	 echo json_encode($aircraft);
	 
	 close_connection();
 }else
 {
	$app->response()->status(401);
	$app->response()->header("Content-Type", "text/plain");
 }
});

//Get cameras
$app->get("/cameras/:user/:password", function($user, $password) use($app)
{
$type = authenticate_user($user, $password);
 
 if(connect($type))
 { 
	 
	$result = mysql_query("SELECT * FROM cameras");
	$cameras = array();

	 while($row = mysql_fetch_array($result))
	 {
		 $cameras[] = array("ID"=> $row['ID'],
							 "Model"=> $row['Model'],
							 "HorizontalRes"=> $row['HorizontalRes'],
							 "VerticalRes"=> $row['VerticalRes'],
							 "FocalLength"=> $row['FocalLength']);
	 }

	 $app->response()->header("Content-Type", "application/json");
	 echo json_encode($cameras);
	 
	 close_connection();
 }else
 {
	$app->response()->status(401);
	$app->response()->header("Content-Type", "text/plain");
 }
});

//Get target types
$app->get("/targettypes/:user/:password", function($user, $password) use($app)
{
$type = authenticate_user($user, $password);
 
 if(connect($type))
 { 
	 
	$result = mysql_query("SELECT * FROM target_types");
	$targets = array();

	 while($row = mysql_fetch_array($result))
	 {
		 $targets[] = array("ID"=> $row['ID'],
						  "TargetName"=> $row['TargetName'],
						  "Description"=> $row['Description']);
	 }

	 $app->response()->header("Content-Type", "application/json");
	 echo json_encode($targets);
	 
	 close_connection();
 }else
 {
	$app->response()->status(401);
	$app->response()->header("Content-Type", "text/plain");
 }
});

//Get number of flights since date and beaches covered
$app->get("/missions/flightstats/:user/:password/:earliest", function($user, $password, $earliest) use($app)
{
	 $type = authenticate_user($user, $password);
	 
	 if(connect($type))
	 { 
	    //get recent sightings first
	    $sql = "SELECT TargetTypeID, MAX( MissionID ) AS max_mission, MAX( DateRecorded ) AS date_recorded
				FROM mission_points
				WHERE targettypeid >1
				GROUP BY targettypeid
				ORDER BY date_recorded DESC";
		$result =  mysql_query($sql);
		$topTargets = array();
		
		if($result)
		{		
			while($row = mysql_fetch_array($result))
			{
			  
			  $sql = "SELECT LocationID from missions where ID = ".$row['max_mission'];
			  $locationResult =  mysql_query($sql);
			  if($locationResult)
			  {
			  		$locationRow = mysql_fetch_array($locationResult);
			  		
			        $topTargets[] = array("TargetTypeID"=>$row['TargetTypeID'],
									      "MissionID"=>$row['max_mission'],
									      "DateRecorded"=>$row['date_recorded'],
									      "LocationID"=>$locationRow['LocationID']);
			  }
			}		 
	 
			//now get flight stats
		 
			$sql = "SELECT Count(ID) as MissionCount, sum(DistanceFlown) as TotalDistance from missions";
					 
			if($earliest != strtolower("all"))
			{
				$sql = $sql. " where DateFlown >= '$earliest';";
			}
			 
			$result =  mysql_query($sql);
			$stats = array();		 
			if($result)
			{
				$mCount = "0";
				$totalDist = "0";
				$beaches = "0";
				
				while($row = mysql_fetch_array($result))
				{
				  $mCount = $row['MissionCount'];
				  $totalDist = $row['TotalDistance'];	  
				}
				
				$sql = "SELECT Count(distinct LocationID) as LocationCount from missions";
				if($earliest != "all")
				{
					$sql = $sql. " where DateFlown >= '$earliest';";
				}
				
				$result =  mysql_query($sql);			 		 
				if($result)
				{
					while($row = mysql_fetch_array($result))
					{
						$beaches = $row['LocationCount'];
					}
					
					$stats[] = array("MissionCount"=> $mCount,
									 "TotalDistance"=> $totalDist,
									 "BeachesCovered"=> $beaches,
									 "LatestTargets"=> $topTargets);
									 
					$app->response()->header("Content-Type", "application/json");
					echo json_encode($stats);
			 
				}
				else
				{
					$app->response()->status(400);
					$app->response()->header("Content-Type", "text/plain");
					echo("Error querying beaches");			
				}
			}
			else
			{
				$app->response()->status(400);
				$app->response()->header("Content-Type", "text/plain");
				echo("Error querying missions");			
			}
		}
		else
		{
			$app->response()->status(400);
			$app->response()->header("Content-Type", "text/plain");
			echo("Error querying beaches");			
		}
		
		close_connection();
	 }else
	 {
		$app->response()->status(401);
		$app->response()->header("Content-Type", "text/plain");
		echo("Authenticaton error for user " + $user);
	 }
 });

 //Get most recent target sightings broken down by target type
$app->get("/missions/latesttargets/:user/:password", function($user, $password) use($app)
{
	 $type = authenticate_user($user, $password);
	 
	 if(connect($type))
	 { 	  
		$sql = "Select mp.TargetTypeID, m.LocationID, mp.MissionID, max(mp.DateRecorded) as date_recorded from mission_points as mp, missions as m where m.ID = mp.MissionID and targettypeid > 1 group by targettypeid order by date_recorded desc";
		$result =  mysql_query($sql);
		if($result)
		{		
			$topTargets = array();
			while($row = mysql_fetch_array($result))
			{
			  $topTargets[] = array("TargetTypeID"=>$row['TargetTypeID'],
									"MissionID"=>$row['MissionID'],
									"DateRecorded"=>$row['date_recorded'],
									"LocationID"=>$row['LocationID']);
			}
			
			$app->response()->header("Content-Type", "application/json");
			echo json_encode($topTargets);
		}else
		{
			$app->response()->status(400);
			$app->response()->header("Content-Type", "text/plain");
			echo("Error querying database");
		}
		 
		close_connection();
	 }else
	 {
		$app->response()->status(401);
		$app->response()->header("Content-Type", "text/plain");
		echo("Authenticaton error for user " + $user);
	 }
 });
 
 //Get mission data
$app->get("/missions/:user/:password/:locationid/:earliest/:detected", function($user, $password, $locationID, $earliest, $targetDetected) use($app)
{
	 $type = authenticate_user($user, $password);
	 
	 if(connect($type))
	 { 
		 $sql = "";
		 if(is_numeric($locationID))
		 {
		
			 if($locationID == 133 && strtolower($earliest) == "all")
			 {
				$sql = "SELECT * FROM missions";
			 }
			 else if(strtolower($earliest) == "all")
			 {
				$sql = "SELECT * FROM missions where LocationID = $locationID";
			 }
			 else if($locationID == 133)
			 {
				$sql = "SELECT * FROM missions where DateFlown >= '$earliest'";
			 }
			 else
			 {
				$sql =  "SELECT * FROM missions where LocationID = $locationID and DateFlown >= '$earliest'";
			 }		 
			 $missions = array();
			 $result =  mysql_query($sql);
			 while($row = mysql_fetch_array($result))
			 {	
				 if(strtolower($targetDetected) == "false" ||
				   (strtolower($targetDetected) == "true" && $row['TargetsDetected'] > 0))
				{
					 $missionPoints = array();
					 $missionPtsResult = mysql_query("SELECT * FROM mission_points where MissionID = ".$row['ID']);
				
					 if($missionPtsResult)
					 {
						 while($pointsRow = mysql_fetch_array($missionPtsResult))
						 { 	
						   $missionPoints[] = array("ID"=> $pointsRow['ID'], 
													 "PointNum"=> $pointsRow['PointNum'], 
													 "DateRecorded"=>$pointsRow['DateRecorded'],
													 "XCoord" => $pointsRow['XCoord'], 
													 "YCoord" => $pointsRow['YCoord'], 
													 "ZCoord" => $pointsRow['ZCoord'], 
													 "TargetDetected"=>$pointsRow['TargetDetected'],
													 "TargetTypeID"=>$pointsRow['TargetTypeID'],
													 "Annotation"=>$pointsRow['Annotation'],
													 "ImageURL"=>$pointsRow['ImageURL'],
													 "WindSpeed"=>$pointsRow['WindSpeed'],
													 "WindBearing"=>$pointsRow['WindBearing']);
						}
					
					
					    $missions[] = array("ID"=> $row['ID'], 
										 "LocationID"=> $row['LocationID'], 
										 "DateFlown" => $row['DateFlown'], 
										 "Duration"=>$row['Duration'],
										 "DistanceFlown"=>$row['DistanceFlown'],
										 "TargetsDetected"=>$row['TargetsDetected'],
										 "PointCount"=>$row['PointCount'],
										 "Description"=>$row['Description'],
										 "MissionPoints"=>$missionPoints
										 );
					}
				}
				
			 }
	
			 $app->response()->header("Content-Type", "application/json");
			 echo json_encode($missions);
		 }else{
		     $app->response()->status(401);
		     $app->response()->header("Content-Type", "application/json");
		     $resp = array();
		     $resp[] = array("Error"=> "LocationID does not exist");
			 echo json_encode($resp);
		 }
		 
		 close_connection();
	 }else
	 {
		$app->response()->status(401);
		$app->response()->header("Content-Type", "application/json");
		$resp = array();
		$resp[] = array("Error"=> "Authenticaton error for user " + $user);
		echo json_encode($resp);
	 }
 });

 //add a location
 $app->post("/location/:user/:password/:location/:town/:locState/:MinX/:MinY/:MaxX/:MaxY", function($user, $password, $location, $town, $locState, $MinX, $MinY, $MaxX, $MaxY) use($app)
 {
    $user = $app->request()->post('username');
	$password = $app->request()->post('password');
	$mission = $app->request()->post('mission');
	
    $type = authenticate_user($user, $password);
	if($type == "PILOT")
    {	
		if(connect($type))
		{ 
		  if(mysql_query("INSERT INTO locations (LocationName, Town, LocState, Country, MinX, MinY, MaxX, MaxY) VALUES ('$location', '$town', '$locState', 'Australia', $MinX, $MinY, $MaxX, $MaxY);"))
		  {
		 
			  $app->response()->header("Content-Type", "application/json");
			  $response = array();
			  $response[] = array("LocationID"=>mysql_insert_id());
			  echo json_encode($response);
		  }else{
		     $app->response()->status(400);
			 $app->response()->header("Content-Type", "text/plain");
		  }
		  close_connection();
		}else
		{
			$app->response()->status(400);
			$app->response()->header("Content-Type", "text/plain");
		}
	}else
	{
	  $app->response()->status(401);
	  $app->response()->header("Content-Type", "text/plain");
	}
});

//add a mission
 $app->post("/mission/new", function() use($app)
 {
    $user = $app->request()->post('username');
	$password = $app->request()->post('password');
	$mission = $app->request()->post('mission');	
		
    $type = authenticate_user($user, $password);
	if($type == "PILOT")
    {	
		if(connect($type))
		{ 
		    $missionData = json_decode($mission, true);	    
		    if(count($missionData) > 0 && array_key_exists("MissionPoints", $missionData))
		    {				
				$dateFlown = convertJSONtoPHPDate($missionData[DateFlown]);
				//echo($date);
				if(mysql_query("INSERT INTO missions (LocationID, DateFlown, PilotID, AircraftID, CameraID, Duration, DistanceFlown, TargetsDetected,PointCount,Description,MissionVideo, MissionLog)
						   VALUES ($missionData[LocationID], '$dateFlown', $missionData[PilotID], $missionData[AircraftID], $missionData[CameraID], $missionData[Duration], 
								   $missionData[DistanceFlown], $missionData[TargetsDetected], $missionData[PointCount], '$missionData[Description]','$missionData[MissionVideo]','$missionData[MissionLog]')"))
			    {
			  
				  $missionID = mysql_insert_id();
				 
				  $missionPoints = $missionData['MissionPoints'];
				  $ptsAdded = true;
				  foreach($missionPoints as $pt)
				  {
				
					$targetTypeID = "null";
					if($pt[TargetTypeID] > 0)
					{
						$targetTypeID = $pt['TargetTypeID'];
					}
					
					$imageURL = "null";
					$imageIndex = 0;
					if(array_key_exists("ImageIndex", $pt))
					{
						$imageIndex = $pt['ImageIndex'];						
						if($imageIndex > 0 && array_key_exists("Image", $pt))
						{
							if($pt['Image'] != null)
							{
								//echo("pt index = ".$imageIndex);
								$imageURL = save_image($pt['Image']);
								//var_dump($imageURL);
								$imageURL = "'".$imageURL."'";
							}				
						}
					}
					
					$timeStampPoint = convertJSONtoPHPDate($pt[DateRecorded]);
					if(!mysql_query("INSERT INTO mission_points (MissionID, PointNum, XCoord, YCoord, ZCoord, TargetDetected, TargetTypeID, Annotation, DateRecorded, ImageIndex, ImageURL, WindSpeed, WindBearing) 
								 VALUES ($missionID, $pt[PointNum], $pt[XCoord], $pt[YCoord], $pt[ZCoord], $pt[TargetDetected],$targetTypeID, '$pt[Annotation]', '$timeStampPoint', $imageIndex, $imageURL, $pt[WindSpeed], $pt[WindBearing])"))
					 {
					   $app->response()->status(400);
					   $app->response()->header("Content-Type", "text/plain");
					   echo("Failed to add mission point $pt[PointNum] to database");	
					   $ptsAdded = false;
					   break;					   
					 }
					
				  }
				  
				  if($ptsAdded)
				  {
					  $app->response()->header("Content-Type", "application/json");
					  $response = array();
					  $response[] = array("ID"=>$missionID);
					  echo json_encode($response);
				  }
			  }else{
			 	 $app->response()->status(400);
			 	 $app->response()->header("Content-Type", "text/plain");
			 	 echo("Failed to add mission to database");
			  }
		    }else{
			  $app->response()->status(400);
			  $app->response()->header("Content-Type", "text/plain");
			  echo("Malformed json data structure");
		    }
		    
		    close_connection();
		}else{
			$app->response()->status(400);
			$app->response()->header("Content-Type", "text/plain");
			echo("No db connection");
		}
	}else
	{
	  $app->response()->status(401);
	  $app->response()->header("Content-Type", "text/plain");
	  echo("cant validate user");
	} 
});
	 
 
$app->run();
?>