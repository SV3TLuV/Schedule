//
//  Time.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct Time: Identifiable, Codable, Hashable, Equatable {
    var id: Int
    var start: String
    var end: String
    var duration: Int
    var lessonNumber: Int
    var type: TimeType
    var isDeleted: Bool
    
    func getRange() -> String {
        return "\(start)-\(end)"
    }
}
