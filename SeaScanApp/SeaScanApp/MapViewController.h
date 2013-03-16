//
//  FirstViewController.h
//  SharkScan
//
//  Created by James Masterman on 25/10/12.
//  Copyright 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <CoreLocation/CoreLocation.h>
#import <MapKit/MapKit.h>
#import "SSDataManager.h"

@interface MapViewController : UIViewController
<CLLocationManagerDelegate, MKMapViewDelegate>
{
    CLLocationManager* locationManager;
    
    __weak IBOutlet MKMapView* mapView;
    __weak IBOutlet UIBarButtonItem* locationTitle;
    __weak IBOutlet UIButton* showCloseOnlyButton;
    __weak IBOutlet UILabel* headerTopLine;
    __weak IBOutlet UILabel* headerLowerLine;
    __weak IBOutlet UISlider* timeSlider;
    __weak IBOutlet UIActivityIndicatorView* activityIndicator;
    
    UILabel* sliderPopUp;
    NSTimer *timer;
    
    CLLocationCoordinate2D loc;
    
    NSNumber* minKey;
    NSNumber* selectedKey;
    NSNumber* lastSelectedKey;
    
    BOOL hasSetUserLocation;
    
}
- (IBAction)filterScans:(id)sender;
- (IBAction) refreshData:(id)sender;
- (IBAction) showTargetsInRange:(id)sender;
- (IBAction) sliderDidEndSliding:(id)sender;
- (IBAction) sliderValueChanged:(id)sender;

- (void) updateTitleFromMission:(NSNumber*) missionID;
- (void) updateTitles;
- (void) refreshScans;
- (void) reloadMissions;
- (void) dePopView;



@end
