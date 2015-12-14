//
//  RBLAppDelegate.m
//  SimpleChat
//
//  Created by redbear on 14-4-8.
//  Copyright (c) 2014年 redbear. All rights reserved.
//

#import "RBLAppDelegate.h"

@implementation RBLAppDelegate

@synthesize delegate;

-(void) SendDataToVC:(NSString*) data {
    [delegate sendDataToArduino:data];
}

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
    // Override point for customization after application launch.
    return YES;
}
							
- (void)applicationWillResignActive:(UIApplication *)application{}

- (void)applicationDidEnterBackground:(UIApplication *)application {}


- (void)applicationWillTerminate:(UIApplication *)application{}


- (void)application:(UIApplication *)application didReceiveLocalNotification:(UILocalNotification *)notification
{
    UIAlertView *alertView = [[UIAlertView alloc]initWithTitle:@"Game found!" message:notification.alertBody delegate:self 	cancelButtonTitle:@"Accept" otherButtonTitles:@"Decline", nil];
    [alertView show];
}

- (void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger	)buttonIndex {
    
//    NSString* backMessage = @"Hey arduino";
//    NSData* backData = [backMessage dataUsingEncoding:NSUTF8StringEncoding];
//    
//    if (bleShield.activePeripheral.state == CBPeripheralStateConnected) {
//        [bleShield write: backData];
//    }
    
    if (buttonIndex == [alertView cancelButtonIndex]) { //Accept
        NSLog(@"Match accepted!");
        NSData* backData = [@"Y" dataUsingEncoding:NSUTF8StringEncoding];
        if (bleShield.activePeripheral.state == CBPeripheralStateConnected) {
            [bleShield write: backData];
        }
        
        
    } else {
        NSLog(@"Match declined!"); //Decline
        NSData* backData = [@"N" dataUsingEncoding:NSUTF8StringEncoding];
        if (bleShield.activePeripheral.state == CBPeripheralStateConnected) {
            [bleShield write: backData];
        }
    }
}

- (void)applicationWillEnterForeground:(UIApplication *)application
{
    // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
    NSLog(@"%s", __PRETTY_FUNCTION__);
    application.applicationIconBadgeNumber = 0;
}

- (void)applicationDidBecomeActive:(UIApplication *)application
{
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
    NSLog(@"%s", __PRETTY_FUNCTION__);
    application.applicationIconBadgeNumber = 0;
}

@end
